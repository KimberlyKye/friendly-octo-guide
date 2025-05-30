using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories
{
    public class TeacherCalendarRepository : ITeacherCalendarRepository
    {
        private readonly AppDbContext _context;
        private readonly ITeacherFactory _teacherFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;

        public TeacherCalendarRepository(
            AppDbContext context,
            ITeacherFactory teacherFactory,
            ICourseFactory courseFactory,
            ILessonFactory lessonFactory)
        {
            _context = context;
            _teacherFactory = teacherFactory;
            _courseFactory = courseFactory;
            _lessonFactory = lessonFactory;
        }
        public async Task<IReadOnlyCollection<Entities.Course>> GetPeriodCalendarData(int teacherId, DateOnly startDate, DateOnly endDate)
        {            
            var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            var endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

            var data = await (
                from teacher in _context.Users.AsNoTracking()
                from course in _context.Courses.Where(c => c.TeacherId == teacher.Id)
                from lesson in _context.Lessons.Where(l => l.CourseId == course.Id
                            && DateOnly.FromDateTime(l.Date) >= startDate
                            && DateOnly.FromDateTime(l.Date) <= endDate)
                where teacher.Id == teacherId
                            && teacher.RoleId == (int)RoleEnum.Teacher
                select new
                {
                    Teacher = teacher,
                    Course = course,
                    Lesson = lesson
                })
                .ToListAsync();

            // Группируем данные по курсам
            var groupedData = data
                .GroupBy(x => x.Course.Id)
                .Select(g => new
                {
                    Course = g.First().Course,
                    Teacher = g.First().Teacher,
                    Lessons = g.Select(x => x.Lesson).ToList()
                });

            var result = new List<Entities.Course>();

            foreach (var group in groupedData)
            {                
                var domainCourse = await _courseFactory.CreateFrom(group.Course, group.Teacher);

                var domainLessons = group.Lessons
                    .Select(lesson => _lessonFactory.CreateAsync(lesson, null).Result)
                    .ToList();
                domainCourse.AddLessons(domainLessons);
                result.Add(domainCourse);                
            }
            return result.AsReadOnly();            
        }
    }
}
