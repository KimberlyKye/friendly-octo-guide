using Entities;
using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;

namespace Application.Services.Abstractions
{
    public interface ITeacherCalendarService
    {
        public Task<TeacherCalendarResponseModel> GetPeriodCalendarData(GetTeacherCalendarDataRequestModel request);        
    }
}
