using Application.Models.Teacher.Requests;

namespace Application.Services.Abstractions
{
    public interface ITeacherLessonService
    {
        public Task<int> CreateLesson(CreateLessonModel requestDto);

        //public Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto);
    }
}
