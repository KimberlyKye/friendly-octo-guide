using Entities;
using Application.Models.Calendar.Requests;

namespace Application.Services.Abstractions
{
    public interface ITeacherCalendarService
    {
        public Task<int> GetWeekCalendarData(GetTeacherCalendarDataRequestModel teacherId);        
    }
}
