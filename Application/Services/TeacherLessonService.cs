using Infrastructure.Repositories.Abstractions;
using Application.Services.Abstractions;
using Entities;
using Domain.ValueObjects;
using Application.Models.Teacher.Requests;
using Application.Models.Teacher.Responses;

namespace Application.Services;

public class TeacherLessonService : ITeacherLessonService
{
    private readonly ITeacherLessonRepository _teacherLessonRepository;
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly ITeacherInfoRepository _teacherInfoRepository;

    public TeacherLessonService(
        ITeacherLessonRepository teacherLessonRepository, 
        ICourseInfoRepository courseInfoRepository,
        ITeacherInfoRepository teacherInfoRepository)
    {
        _teacherLessonRepository = teacherLessonRepository;
        _courseInfoRepository = courseInfoRepository;
        _teacherInfoRepository = teacherInfoRepository;
    }
    //public async Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto)
    //{
    //    return await _teacherRepository.GetCalendarData(requestDto);
    //}
    public async Task<int> CreateLesson(CreateLessonModel requestDto)
    {
        Teacher teacher = await _teacherInfoRepository.GetTeacherById(requestDto.TeacherId);

        if (!await _courseInfoRepository.CheckIsCourseExistAndActiveById(requestDto.CourseId))
        { return -1; }

        Lesson newLesson = await teacher.CreateLesson(  0,
                                                        requestDto.CourseId,
                                                        requestDto.LessonName,
                                                        requestDto.LessonDescription,
                                                        requestDto.LessonStartDate,
                                                        requestDto.Material,
                                                        requestDto.HomeTasks);

        return await _teacherLessonRepository.AddLesson(newLesson);
    }    
}
