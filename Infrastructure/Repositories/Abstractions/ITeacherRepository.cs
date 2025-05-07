using Dto.Teacher.Requests;
using Dto.Teacher.Responses;
using Entities;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository
    {
        public Task<Teacher> GetTeacherById(int teacherId);
        Task<bool> CheckIsCourseExistAndActiveById(int courseId);
        Task<int> AddLesson(Entities.Lesson lesson);
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);

    }
}
