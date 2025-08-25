using Common.Domain.ValueObjects.Enums;
using Common.Models.Calendar.Responses;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

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
        public async Task<CalendarLessonModel[]> GetLessonsAsync(int studentId, DateTime startDate, DateTime endDate)
        {
            var universalStart = startDate.ToUniversalTime();
            var universalEnd = endDate.ToUniversalTime();

            return await (
                from student in _context.Users.AsNoTracking()
                from studentsCourses in _context.StudentCourses.Where(sCs => sCs.StudentId == student.Id)
                from courses in _context.Courses.Where(c => c.Id == studentsCourses.CourseId
                                                         && c.StateId == (int)CourseState.Active)
                from lessons in _context.Lessons.Where(l => l.CourseId == courses.Id
                                                         && l.Date >= universalStart
                                                         && l.Date <= universalEnd)
                where student.Id == studentId
                   && student.RoleId == (int)RoleEnum.Student
                select new CalendarLessonModel
                {
                    CourseId = courses.Id,
                    CourseName = courses.Title,
                    LessonId = lessons.Id,
                    LessonName = lessons.Title,
                    LessonDate = lessons.Date
                })
                .ToArrayAsync();
        }

        public async Task<CalendarHomeTaskModel[]> GetHomeTasksAsync(int studentId, DateTime startDate, DateTime endDate)
        {
            var universalStart = startDate.ToUniversalTime();
            var universalEnd = endDate.ToUniversalTime();

            return await (
                from student in _context.Users.AsNoTracking()
                from studentsCourses in _context.StudentCourses.Where(sCs => sCs.StudentId == student.Id)
                from courses in _context.Courses.Where(c => c.Id == studentsCourses.CourseId
                                                         && c.StateId == (int)CourseState.Active)
                from lessons in _context.Lessons.Where(l => l.CourseId == courses.Id)
                from homeTasks in _context.HomeTasks.Where(ht => ht.LessonId == lessons.Id
                                                              && ht.EndDate >= universalStart
                                                              && ht.StartDate <= universalEnd)
                where student.Id == studentId
                   && student.RoleId == (int)RoleEnum.Student
                select new CalendarHomeTaskModel
                {
                    CourseId = courses.Id,
                    CourseName = courses.Title,
                    LessonId = lessons.Id,
                    LessonName = lessons.Title,
                    HomeTaskId = homeTasks.Id,
                    HomeTaskName = homeTasks.Title,
                    DateStart = homeTasks.StartDate,
                    DateEnd = homeTasks.EndDate
                })
                .ToArrayAsync();
        }
    }
}
