using Application.Models.Course;
using Application.Services.Abstractions;
using Entities;
using Microsoft.VisualBasic;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services;

public class CourseService : ICourseService
{
    private ICourseRepository _courseRepository;
    private ITeacherInfoRepository _teacherInfoRepository;
    private IStudentInfoRepository _studentInfoRepository;
    public CourseService(
        ICourseRepository courseRepository,
        ITeacherInfoRepository teacherInfoRepository,
        IStudentInfoRepository studentInfoRepository)
    {
        _courseRepository = courseRepository;
        _teacherInfoRepository = teacherInfoRepository;
        _studentInfoRepository = studentInfoRepository;
    }

    public async Task<int> AddCourseAsync(CreateCourseModel request)
    {
        Teacher? teacher = await _teacherInfoRepository.GetTeacherById(request.TeacherId);
        if (teacher is null)
        {
            throw new ArgumentException($"Пользователь с ID {request.TeacherId} не найден", nameof(request.TeacherId));
        }
        Course newCourse = await teacher.CreateCourse(0,
                                                        teacher,
                                                        new Domain.ValueObjects.CourseName(request.Title),
                                                        request.Description,
                                                        new Domain.ValueObjects.Duration(request.StartDate, request.EndDate));

        return await _courseRepository.AddCourseAsync(newCourse);
    }

    public async Task<Course> GetCourseAsync(int courseId)
    {
        var checkedCourse = await _courseRepository.GetCourseAsync(courseId);
        if (checkedCourse is null)
        {
            throw new ArgumentException("Course not found!");
        }

        return checkedCourse;
    }

    public async Task<Course[]> GetAllStudentCoursesAsync(int userId)
    {
        // Проверим, что студент существует
        Student? student = await _studentInfoRepository.GetStudentById(userId);
        if (student is null)
        {
            throw new ArgumentException($"Пользователь с ID {userId} не найден", nameof(userId));
        }

        return await _courseRepository.GetAllStudentCoursesAsync(student);
    }

    public async Task<Course[]> GetAllTeacherCoursesAsync(int userId)
    {
        // Проверим, что преподаватель существует
        Teacher? teacher = await _teacherInfoRepository.GetTeacherById(userId);

        if (teacher is null)
        {
            throw new ArgumentException($"Пользователь с ID {userId} не найден", nameof(userId));
        }

        return await _courseRepository.GetAllTeacherCoursesAsync(teacher);
    }

    public async Task<int> UpdateCourseAsync(Course course)
    {
        // Проверим, что курс существует
        _ = await GetCourseAsync(course.Id);

        return await _courseRepository.UpdateCourseAsync(course);
    }

    public async Task<Lesson> GetLessonByIdAsync(int id)
    {
        var checkedLesson = await _courseRepository.GetLessonByIdAsync(id);
        if (checkedLesson is null)
        {
            throw new ArgumentException("Lesson not found!");
        }

        return checkedLesson;
    }

    public async Task<int> UpdateLesson(Lesson lesson)
    {
        // Проверим, что урок существует
        _ = await GetLessonByIdAsync(lesson.Id);

        return await _courseRepository.UpdateLesson(lesson);
    }
}