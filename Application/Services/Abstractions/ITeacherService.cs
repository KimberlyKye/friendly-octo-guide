using Entities;
using Dto.Teacher.Requests;
using Dto.Teacher.Responses;
using Application.Dto.Teacher;

namespace Application.Services.Abstractions
{
    public interface ITeacherService
    {
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);
        public Task<int> CreateLesson(CreateLessonModel requestDto);
        public Task<Teacher> GetTeacherById(int teacherId);
    }
}
