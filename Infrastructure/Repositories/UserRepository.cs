using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;
        private readonly ITeacherFactory _teacherFactory;
        public UserRepository(
            AppDbContext context,
            IStudentFactory studentFactory,
            ITeacherFactory teacherFactory
        )
        {
            _context = context;
            _studentFactory = studentFactory;
            _teacherFactory = teacherFactory;
        }

        public async Task<RoleEnum?> GetUserRoleAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                return (RoleEnum)user.RoleId;
            }
            return null;
        }

    }
}