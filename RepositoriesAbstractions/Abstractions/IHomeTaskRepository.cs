using Entities;

namespace RepositoriesAbstractions;

public interface IHomeTaskRepository
{
    Task<HomeTask?> GetByIdAsync(int id);
}
