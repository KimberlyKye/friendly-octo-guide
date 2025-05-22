using RepositoriesAbstractions.Abstractions;
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
        if (requestDto.LessonStartDate <= DateTime.Now)
        {
            throw new ArgumentException("При создании занятия дата проведения не может быть в прошлом");
        }

        Teacher? teacher = await _teacherInfoRepository.GetTeacherById(requestDto.TeacherId);
        if (teacher is null)
        {
            throw new ArgumentException($"Преподаватель с ID {requestDto.TeacherId} не существует", nameof(requestDto.TeacherId));
        }

        if (!await _courseInfoRepository.CheckIsCourseExistAndActiveById(requestDto.CourseId))
            { throw new ArgumentException($"Курс с ID {requestDto.CourseId} не существует или архтвный", nameof(requestDto.CourseId)); }

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
