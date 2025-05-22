using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface ITeacherCalendarRepository
    {
        public Task<IReadOnlyCollection<Course>?> GetPeriodCalendarData(int teacherId,DateOnly startDate,DateOnly endDate);
    }
}
