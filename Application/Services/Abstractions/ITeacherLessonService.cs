using Entities;
using Application.Models.Teacher.Requests;
using Application.Models.Teacher.Responses;

namespace Application.Services.Abstractions
{
    public interface ITeacherLessonService
    {        
        public Task<int> CreateLesson(CreateLessonModel requestDto);

        //public Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto);
    }
}
