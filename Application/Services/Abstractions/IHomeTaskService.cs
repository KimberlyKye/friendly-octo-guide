using Common.Domain.Entities;

namespace Application.Services.Abstractions
{
    public interface IHomeTaskService
    {
        Task<HomeTask> GetByIdAsync(int homeTaskId);
    }
}