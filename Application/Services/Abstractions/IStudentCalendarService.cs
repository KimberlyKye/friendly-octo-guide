using Entities;
using Application.Models.Calendar.Requests;

namespace Application.Services.Abstractions
{
    public interface IStudentCalendarService
    {
        public Task<int> GetCalendarData(GetStudentCalendarDataRequestModel teacherId);        
    }
}
