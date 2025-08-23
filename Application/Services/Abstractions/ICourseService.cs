using Application.Models.Course;
using Entities;

namespace Application.Services.Abstractions;

/// <summary>
/// Сервис для работы с курсами.
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Добавить курс.
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    public Task<int> AddCourseAsync(CreateCourseModel course);

    /// <summary>
    /// Получить курс по id.
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public Task<Course> GetCourseAsync(int courseId);

    /// <summary>
    /// Получить все курсы студента.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<Course[]> GetAllStudentCoursesAsync(int userId);

    /// <summary>
    /// Получить все курсы преподавателя.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<Course[]> GetAllTeacherCoursesAsync(int userId);

    /// <summary>
    /// Обновить курс.
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    public Task<int> UpdateCourseAsync(Course course);

    /// <summary>
    /// Получить урок по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public Task<Lesson> GetLessonByIdAsync(int id);

    /// <summary>
    /// Обновить урок.
    /// </summary>
    /// <param name="lesson"></param>
    /// <returns></returns>
    public Task<int> UpdateLesson(Lesson lesson);
}