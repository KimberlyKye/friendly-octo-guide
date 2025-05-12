using Infrastructure.DataModels;

namespace Infrastructure.Repositories.Abstractions;

public interface ICourseRepository
{
    public Task<Course> AddCourseAsync(Course course);
}