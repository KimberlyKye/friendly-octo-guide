using Entities;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ICourseFactory _courseFactory;
    private readonly AppDbContext _context;

    public CourseRepository(
        AppDbContext context,
        ICourseFactory courseFactory
    )
    {
        _context = context;
        _courseFactory = courseFactory;
    }

    public async Task<int> AddCourseAsync(Entities.Course course)
    {
        var courseDataModel = _courseFactory.MapTo(course);

        await _context.Courses.AddAsync(courseDataModel);
        await _context.SaveChangesAsync();
        return courseDataModel.Id;
    }
}