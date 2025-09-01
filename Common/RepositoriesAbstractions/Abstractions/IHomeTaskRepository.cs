using Common.Domain.Entities;

namespace Common.RepositoriesAbstractions;

public interface IHomeTaskRepository
{
    /// <summary>
    /// Получить задачу по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<HomeTask?> GetByIdAsync(int id);
}
