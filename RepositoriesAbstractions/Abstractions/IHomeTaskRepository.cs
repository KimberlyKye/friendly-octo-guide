using Entities;

namespace RepositoriesAbstractions;

public interface IHomeTaskRepository
{
    /// <summary>
    /// Получить задачу по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<HomeTask?> GetByIdAsync(int id);
}
