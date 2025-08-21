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
    
    public async Task<int> CreateLesson(CreateLessonModel request)
    {
        Teacher? teacher = await _teacherInfoRepository.GetTeacherById(request.TeacherId);
        if (teacher is null) 
        {
            throw new ArgumentNullException($"Преподаватель с ID {request.TeacherId} не существует", nameof(request.TeacherId));
        }
        if (!await _courseInfoRepository.CheckIsCourseExistAndActiveById(request.CourseId))
        {
            throw new ArgumentNullException($"Курс с ID {request.CourseId} не существует или уже в архиве", nameof(request.CourseId));
        }
        if (!await _courseInfoRepository.IsCourseOwnedByTeacherAsync(request.CourseId, teacher.Id))
        {
            throw new ArgumentNullException($"Курс с ID {request.CourseId} не не принадлежит преподавателю с ID {request.TeacherId}", nameof(request.CourseId));
        }

        Lesson newLesson = await teacher.CreateLesson(  0,
                                                        request.CourseId,
                                                        request.LessonName,
                                                        request.LessonDescription,
                                                        request.LessonStartDate,
                                                        request.Material,
                                                        request.HomeTask);

        return await _teacherLessonRepository.AddLesson(newLesson);
    }    
}
