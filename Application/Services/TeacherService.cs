using Infrastructure.Repositories.Abstractions;
using Application.Services.Abstractions;
using Dto.Teacher.Requests;
using Dto.Teacher.Responses;

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
    public async Task<int> CreateLesson(CreateLessonRequestDto requestDto)
    {
        // DTO ----> Model
        //Teacher ----> Teacher.AddLesson(courseId, lessonData) ---> Lesson(Domain)
        //var result = await _teacherRepository.CreateLesson(Lesson(Domain);

        return 1;
    }
}
