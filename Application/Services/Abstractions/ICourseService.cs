using Application.Models.Course;

namespace Application.Services.Abstractions;

public interface ICourseService
{
    public Task<int> AddCourseAsync(CreateCourseModel course);
}