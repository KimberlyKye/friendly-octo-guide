using Entities;
using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;

namespace Application.Services.Abstractions
{
    public interface IStudentCalendarService
    {
        public Task<StudentCalendarResponseModel> GetPeriodCalendarData(GetStudentCalendarDataRequestModel teacherId);        
    }
}
