using Common.Domain.Entities;
using Common.Models.Calendar.Responses;

namespace RepositoriesAbstractions.Abstractions
{
    public interface ITeacherCalendarRepository
    {
        public Task<TeacherCalendarResponseModel> GetPeriodCalendarData(int teacherId, DateTime startDate, DateTime endDate);
    }
}
