using Application.Models.Course;
using Application.Services.Abstractions;
using Infrastructure.DataModels;
using Infrastructure.Repositories.Abstractions;

namespace Application.Services;

public class CourseService : ICourseService
{
    public CourseService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public Task<Course> AddCourseAsync(CreateCourseModel course)
    {
        Course courseModel = new Course()
        {
            StateId = (int)course.StateId,
            TeacherId = course.TeacherId,
            Title = course.Title,
            Description = course.Description,
            StartDate = course.StartDate,
            EndDate = course.EndDate,
            PassingScore = course.PassingScore
        };

       return _repository.AddCourseAsync(courseModel);
    }

    private ICourseRepository _repository;
}