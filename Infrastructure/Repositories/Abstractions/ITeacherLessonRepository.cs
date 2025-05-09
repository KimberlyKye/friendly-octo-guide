using Entities;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ITeacherLessonRepository
    {
        Task<int> AddLesson(Entities.Lesson lesson);
        //public Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto);

    }
}
