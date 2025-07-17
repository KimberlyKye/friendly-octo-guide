using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentCalendarRepository
    {
        public Task<IReadOnlyCollection<Course>> GetPeriodCalendarData(int studentId, DateTime startDate, DateTime endDate);
    }
}
