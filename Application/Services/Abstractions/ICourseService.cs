using Application.Models.Course;
using Infrastructure.DataModels;

namespace Application.Services.Abstractions;

public interface ICourseService
{
    public Task<Course> AddCourseAsync(CreateCourseModel course);
}