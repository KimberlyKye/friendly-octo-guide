using Common.Domain.ValueObjects.Enums;
using Common.Models.Calendar.Responses;
using Infrastructure.Contexts;
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
        public async Task<TeacherCalendarResponseModel> GetPeriodCalendarData(int teacherId, DateTime startDate, DateTime endDate)
        {
            var calendarLessons = await (
                from teacher in _context.Users.AsNoTracking()
                from course in _context.Courses.Where(c => c.TeacherId == teacher.Id
                                                        && c.StateId == (int)CourseState.Active)
                from lesson in _context.Lessons.Where(l => l.CourseId == course.Id
                                                        && l.Date >= startDate.ToUniversalTime()
                                                        && l.Date <= endDate.ToUniversalTime())
                where teacher.Id == teacherId 
                   && teacher.RoleId == (int)RoleEnum.Teacher
                select new CalendarLessonModel
                {
                    CourseId = course.Id,
                    CourseName = course.Title,
                    LessonId = lesson.Id,
                    LessonDate = lesson.Date,
                    LessonName = lesson.Title
                })
                .ToArrayAsync();

            return new TeacherCalendarResponseModel
            {
                CalendarLessonModels = calendarLessons
            };
        }
    }
}
