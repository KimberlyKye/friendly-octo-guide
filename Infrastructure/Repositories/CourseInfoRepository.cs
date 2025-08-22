using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseInfoRepository : ICourseInfoRepository
    {
        private readonly AppDbContext _context;
        public CourseInfoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckIsCourseExistAndActiveById(int courseId)
        {
            return await _context.Courses
                .AnyAsync(c => c.Id == courseId && c.StateId == (int)CourseState.Active);
        }

        public async Task<bool> IsCourseOwnedByTeacherAsync(int courseId, int teacherId)
        {
            return await _context.Courses
                .AnyAsync(c => c.Id == courseId && c.TeacherId == teacherId);
        }
    }
}
