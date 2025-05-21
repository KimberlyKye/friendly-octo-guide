using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using RepositoriesAbstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories.Abstractions;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    public class TeacherInfoRepository : ITeacherInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly ITeacherFactory _teacherFactory;

        public TeacherInfoRepository(
            AppDbContext context,
            ITeacherFactory teacherFactory)
        {
            _context = context;
            _teacherFactory = teacherFactory;

        }
        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            try
            {
                var teacherInfo = await _context.Users
                    .Where(teacher => teacher.Id == teacherId && teacher.RoleId == (int)RoleEnum.Teacher)
                    .FirstOrDefaultAsync();

                if (teacherInfo == null) { throw new Exception(); }

                return await _teacherFactory.CreateFrom(teacherInfo);
            }
            catch
            {
                //_logger.LogError(ex, "Error while getting teacher by ID {TeacherId}", teacherId);
                throw;
            }
        }
    }
}
