using Application.Models.Course;
using Application.Services.Abstractions;
using Infrastructure.DataModels;
using Microsoft.VisualBasic;
using RepositoriesAbstractions.Abstractions;

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

        throw new Exception("Нужно выправлять логику");

        //return _repository.AddCourseAsync(courseModel);
    }

    private ICourseRepository _repository;
}