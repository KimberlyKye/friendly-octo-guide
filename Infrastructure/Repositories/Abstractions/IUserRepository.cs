using Infrastructure.DataModels;

namespace Infrastructure.Repositories.Abstractions;

public interface IUserRepository
{
    public Task<User> GetUserByEmailAsync(string email);
}