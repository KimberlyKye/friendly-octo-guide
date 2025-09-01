using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.RepositoriesAbstractions;

namespace Application.Services
{
    public class HomeTaskService : IHomeTaskService
    {
        private readonly IHomeTaskRepository _homeTaskRepository;

        public HomeTaskService(IHomeTaskRepository homeTaskRepository)
        {
            this._homeTaskRepository = homeTaskRepository;
        }

        public Task<HomeTask> GetByIdAsync(int homeTaskId)
        {
            var homeTask = _homeTaskRepository.GetByIdAsync(homeTaskId);

            if (homeTask == null)
            {
                throw new Exception("HomeTask not found");
            }

            return homeTask;
        }
    }
}