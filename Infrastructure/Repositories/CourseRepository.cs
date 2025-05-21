using Infrastructure.Contexts;
using Infrastructure.DataModels;
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

    public async Task<Course> AddCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public Task<Entities.Course> AddCourseAsync(Entities.Course course)
    {
        throw new NotImplementedException();
    }
}