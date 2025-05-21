using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;
using Application.Services.Abstractions;
using RepositoriesAbstractions.Abstractions;
using Entities;

namespace Application.Services
{
    public class TeacherCalendarService : ITeacherCalendarService
    {
        private readonly ITeacherCalendarRepository _teacherCalendarRepository;

        public TeacherCalendarService(ITeacherCalendarRepository teacherCalendarRepository)
        {
            _teacherCalendarRepository = teacherCalendarRepository;
        }
        public async Task<TeacherCalendarResponseModel> GetPeriodCalendarData(GetTeacherCalendarDataRequestModel request)
        {
            IReadOnlyCollection<Course> courses =  await _teacherCalendarRepository.GetPeriodCalendarData(request.TeacherId, request.StartDate, request.EndDate);
            
            var calendarLessons = courses.SelectMany(course =>
                    course.Lessons.Select(lesson => new CalendarLessonModel
                    {
                        CourseId = course.Id,
                        CourseName = course.Name.Value,
                        LessonId = lesson.Id,
                        LessonDate = lesson.Date,
                        LessonName = lesson.Name
                    })).ToArray();

            return new TeacherCalendarResponseModel
            {
                CalendarLessonModels = calendarLessons
            };
        }
    }
}