using Dto;
using Infrastructure.Repositories.Abstractions;
using Application.Services.Abstractions;

namespace Application.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto)
    {
        return await _teacherRepository.GetCalendarData(requestDto);
    }
    //public async Task<CalendarResponseDto> CreateLesson(GetCalendarDataRequestDto requestDto)
    //{
    //    return await _teacherRepository.CreateLesson(requestDto);
    //}
}
