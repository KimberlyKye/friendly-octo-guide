using Application.Models.Course;
using Entities;

namespace Application.Services.Abstractions;

public interface ICourseService
{
    public Task<int> AddCourseAsync(CreateCourseModel course);
}