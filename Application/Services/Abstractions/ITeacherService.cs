using Entities;
using Dto.Teacher.Requests;
using Dto.Teacher.Responses;

namespace Application.Services.Abstractions
{
    public interface ITeacherService
    {
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);
        public Task<int> CreateLesson(CreateLessonRequestDto requestDto);
    }
}
