using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;

namespace Application.Services.Abstractions
{
    public interface ITeacherCalendarService
    {
        public Task<TeacherCalendarResponseModel> GetPeriodCalendarData(GetTeacherCalendarDataRequestModel request);
    }
}
