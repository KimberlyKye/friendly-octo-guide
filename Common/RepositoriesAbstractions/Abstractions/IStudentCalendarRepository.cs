using Common.Domain.Entities;
using Common.Models.Calendar.Responses;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentCalendarRepository
    {
        public Task<CalendarLessonModel[]> GetLessonsAsync(int studentId, DateTime startDate, DateTime endDate);
        public Task<CalendarHomeTaskModel[]> GetHomeTasksAsync(int studentId, DateTime startDate, DateTime endDate);
    }
}
