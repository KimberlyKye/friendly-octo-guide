using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == email);

        if (user == null)
        {
            throw new InvalidOperationException("Профиль студента не найден.");
        }

        return user;
    }

    private readonly AppDbContext _context;
}