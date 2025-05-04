using Infrastructure.Repositories.Abstractions;
using Application.Services.Abstractions;
using Dto.Teacher.Requests;
using Dto.Teacher.Responses;
using Entities;
using Application.Dto.Teacher;
using Domain.ValueObjects;

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
    public async Task<int> CreateLesson(CreateLessonModel requestDto)
    {
        Teacher teacher = await _teacherRepository.GetTeacherById(requestDto.TeacherId);

        //if (!await _teacherRepository.CheckIsRealCourseById(requestDto.CourseId))
        //    { return -1; }    
        
        Lesson newLesson = await teacher.CreateLesson(0,
                                                            requestDto.LessonName,
                                                            requestDto.LessonDescription,
                                                            requestDto.LessonStartDate,
                                                            requestDto.Material,
                                                            requestDto.HomeTasks);

        return await _teacherRepository.AddLesson(newLesson);
    }
    public async Task<Teacher> GetTeacherById(int teacherId)
    {
        return await _teacherRepository.GetTeacherById(teacherId);
    }
}
