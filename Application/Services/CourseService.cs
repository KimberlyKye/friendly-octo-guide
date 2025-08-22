using Application.Models.Course;
using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.RepositoriesAbstractions.Abstractions;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services;

public class CourseService : ICourseService
{
    private ICourseRepository _courseRepository;
    private ITeacherInfoRepository _teacherInfoRepository;
    public CourseService(
        ICourseRepository courseRepository,
        ITeacherInfoRepository teacherInfoRepository)
    {
        _courseRepository = courseRepository;
        _teacherInfoRepository = teacherInfoRepository;
    }

    public async Task<int> AddCourseAsync(CreateCourseModel request)
    {
        Teacher? teacher = await _teacherInfoRepository.GetTeacherById(request.TeacherId);
        if (teacher is null)
        {
            throw new ArgumentException($"Преподаватель с ID {request.TeacherId} не существует", nameof(request.TeacherId));
        }
        Course newCourse = await teacher.CreateCourse(0,
                                                        teacher,
                                                        new Common.Domain.ValueObjects.CourseName(request.Title),
                                                        request.Description,
                                                        new Common.Domain.ValueObjects.Duration(request.StartDate, request.EndDate),
                                                        new Score(request.PassingScore));

        return await _courseRepository.AddCourseAsync(newCourse);
    }
    private ICourseRepository _repository;
}