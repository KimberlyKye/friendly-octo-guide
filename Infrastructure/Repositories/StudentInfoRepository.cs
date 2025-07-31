using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentInfoRepository : IStudentInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;
        private readonly IHomeTaskFactory _homeTaskFactory;


        public StudentInfoRepository(
            AppDbContext context,
            IStudentFactory studentFactory,
            ICourseFactory courseFactory,
            ILessonFactory lessonFactory,
            IHomeTaskFactory homeTaskFactory)
        {
            _context = context;
            _studentFactory = studentFactory;
            _courseFactory = courseFactory;
            _lessonFactory = lessonFactory;
            _homeTaskFactory = homeTaskFactory;
        }

        public async Task<Student?> GetStudentById(int studentId)
        {

            var studentInfo = await _context.Users
                .Where(student => student.Id == studentId && student.RoleId == (int)RoleEnum.Student)
                .FirstOrDefaultAsync();

            if (studentInfo is null) { return null; }

            return await _studentFactory.CreateFromAsync(studentInfo);
        }
        public async Task<List<Entities.Course>> GetAllCourses(int studentId)
        {
            var coursesData = await (
                from student in _context.Users.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.StudentId == student.Id)
                from course in _context.Courses
                    .Where(c => c.Id == studentCourse.CourseId)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where student.Id == studentId
                    && student.RoleId == (int)RoleEnum.Student
                select new { Course = course, Teacher = teacher })
                .ToListAsync();

            var result = new List<Entities.Course>();
            foreach (var item in coursesData)
            {
                var domainCourse = await _courseFactory.CreateFrom(
                    courseModel: item.Course,
                    teacher: item.Teacher);

                result.Add(domainCourse);

            }
            return result;
        }
        public async Task<Entities.Course?> GetCourseInfo(int courseId, int studentId)
        {
            var coursesData = await (
                from course in _context.Courses.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.CourseId == course.Id && sc.StudentId == studentId)
                from lessons in _context.Lessons
                    .Where(l => l.CourseId == courseId)
                from lessonsScores in _context.LessonScores
                    .Where(ls => ls.LessonId == lessons.Id && ls.StudentId == studentId)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where course.Id == courseId
                group lessonsScores.Score by new { course, teacher } into g
                select new
                {
                    Course = g.Key.course,
                    Teacher = g.Key.teacher,
                    Scores = g.ToList() 
                })
                .FirstOrDefaultAsync();

            if (coursesData is null) { return null; }

            // Обработка оценок и создание Score
            Score? averageScore = null;
            if (coursesData.Scores.Any())
            {
                var averageValue = (int)Math.Round(coursesData.Scores.Average());
                if (Score.IsValid(averageValue))
                {
                    averageScore = new Score(averageValue);
                }
            }

            var domainCourse = await _courseFactory.CreateFrom(
                courseModel: coursesData.Course,
                teacher: coursesData.Teacher,
                averageScore: averageScore);

            return domainCourse;
        }
        public async Task<Entities.Course?> GetAllCourseInfo(int courseId, int studentId)
        {
            var coursesData = await (
                from course in _context.Courses.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.CourseId == course.Id
                              && sc.StudentId == studentId)
                from lessons in _context.Lessons
                    .Where(l => l.CourseId == course.Id)
                from lessonScore in _context.LessonScores
                    .Where(lS => lS.LessonId == lessons.Id
                              && lS.StudentId == studentId)
                    .DefaultIfEmpty()
                from homeTasks in _context.HomeTasks
                    .Where(hTs => hTs.LessonId == lessons.Id)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where course.Id == courseId
                select new
                {
                    Teacher = teacher,
                    Course = course,
                    Lesson = new DataModels.Lesson // Создаем Lesson с Score
                    {
                        Id = lessons.Id,
                        CourseId = lessons.CourseId,
                        Title = lessons.Title,
                        Description = lessons.Description,
                        Date = lessons.Date,
                        Material = lessons.Material,
                        Score = lessonScore != null ? lessonScore.Score : 0
                    },
                    HomeTask = homeTasks
                })
                .ToListAsync();

            if (!coursesData.Any())
                return null;

            // Упрощенная обработка результатов
            var firstRecord = coursesData.First();
            var domainCourse = await _courseFactory.CreateFrom(firstRecord.Course, firstRecord.Teacher);

            // Группируем уроки с заданиями
            var lessonsWithTasks = coursesData
                .GroupBy(x => x.Lesson.Id)
                .Select(g => new
                {
                    Lesson = g.First().Lesson, // Уже содержит Score
                    HomeTasks = g.Select(x => x.HomeTask).Where(ht => ht != null).ToList()
                });

            foreach (var lessonGroup in lessonsWithTasks)
            {
                var domainLesson = await _lessonFactory.CreateAsync(
                    lessonGroup.Lesson,
                    lessonGroup.HomeTasks);

                domainCourse.AddLesson(domainLesson);
            }

            return domainCourse;
        }

        public async Task<Score> GetCourseAverageScore(int courseId, int studentId)
        {
            var averageScore = await (
                from course in _context.Courses
                from lessons in _context.Lessons
                    .Where(l => l.CourseId == course.Id)
                from lessonScore in _context.LessonScores
                    .Where(lS => lS.LessonId == lessons.Id &&
                                lS.StudentId == studentId)
                where course.Id == courseId
                select (double?)lessonScore.Score
                )
                .AverageAsync();

            return new Score((int)Math.Round(averageScore ?? 0));
        }        
    }
}
