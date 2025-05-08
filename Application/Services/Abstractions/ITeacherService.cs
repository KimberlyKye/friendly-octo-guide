using Entities;
using Application.Models.Teacher.Requests;
using Application.Models.Teacher.Responses;

namespace Application.Services.Abstractions
{
    public interface ITeacherService
    {        
        public Task<int> CreateLesson(CreateLessonModel requestDto);
        public Task<Teacher> GetTeacherById(int teacherId);

        //public Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto);
    }
}
