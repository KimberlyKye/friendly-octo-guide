using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;

namespace Application.Services.Abstractions
{
    public interface IStudentCalendarService
    {
        public Task<StudentCalendarResponseModel> GetPeriodCalendarData(GetStudentCalendarDataRequestModel teacherId);
    }
}
