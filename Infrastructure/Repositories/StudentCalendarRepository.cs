using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
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
    public class StudentCalendarRepository : IStudentCalendarRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;
        private readonly IHomeTaskFactory _homeTaskFactory;

        public StudentCalendarRepository(
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
        public async Task<IReadOnlyCollection<Entities.Course>> GetPeriodCalendarData(int studentId, DateTime startDate, DateTime endDate)
        {
            var data = await (
                from student in _context.Users.AsNoTracking()
                from studentsCourses in _context.StudentCourses
                    .Where(sCs => sCs.StudentId == student.Id)
                from courses in _context.Courses
                    .Where(c => c.Id == studentsCourses.CourseId
                            && c.StateId == (int)CourseState.Active)
                from teacher in _context.Users
                    .Where(t => t.Id == courses.TeacherId
                            && t.RoleId == (int)RoleEnum.Teacher)
                from lessons in _context.Lessons.Where(l => l.CourseId == courses.Id
                            && l.Date >= startDate.ToUniversalTime()
                            && l.Date <= endDate.ToUniversalTime())
                from homeTasks in _context.HomeTasks.Where(hTs => hTs.LessonId == lessons.Id
                            && hTs.StartDate >= startDate.ToUniversalTime()
                            && hTs.EndDate <= endDate.ToUniversalTime())
                where student.Id == studentId
                            && student.RoleId == (int)RoleEnum.Student
                select new
                {
                    Teacher = teacher,
                    Course = courses,
                    Lesson = lessons,
                    HomeTasks = homeTasks
                })
                .ToListAsync();

            var groupedData = data
                    .GroupBy(x => x.Course.Id)
                    .Select(g => new
                    {
                        Course = g.First().Course,
                        Teacher = g.First().Teacher,
                        Lessons = g
                            .GroupBy(x => x.Lesson.Id)
                            .Select(lg => new
                            {
                                Lesson = lg.First().Lesson,
                                HomeTasks = lg.Select(x => x.HomeTasks).ToList()
                            })
                            .ToList()
                    });

            var result = new List<Entities.Course>();

            foreach (var group in groupedData)
            {
                // Создаем доменный курс
                var domainCourse = await _courseFactory.CreateFrom(group.Course, group.Teacher);
                var domainLessons = new List<Entities.Lesson>();

                foreach (var lessonGroup in group.Lessons)
                {
                    // Получаем DataModels.HomeTask для текущего урока
                    var dataModelHomeTasks = lessonGroup.HomeTasks;

                    // Создаем урок, передавая DataModels.HomeTask
                    var domainLesson = await _lessonFactory.CreateAsync(
                        lessonGroup.Lesson,
                        dataModelHomeTasks);

                    domainLessons.Add(domainLesson);
                }

                domainCourse.AddLessons(domainLessons);
                result.Add(domainCourse);
            }
            return result.AsReadOnly();
        }
    }
}
