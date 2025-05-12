using Infrastructure.DataModels;

namespace Infrastructure.Repositories.Abstractions;

/// <summary>
/// Репозиторий для курса
/// </summary>
public interface ICourseRepository
{
    /// <summary>
    /// Добавить курс
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    public Task<Course> AddCourseAsync(Course course);
}