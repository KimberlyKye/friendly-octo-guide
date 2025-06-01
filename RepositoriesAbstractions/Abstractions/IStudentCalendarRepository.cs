using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentCalendarRepository
    {
        public Task<IReadOnlyCollection<Course>> GetPeriodCalendarData(int studentId, DateOnly startDate, DateOnly endDate);
    }
}
