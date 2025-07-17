using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Entities;
using RepositoriesAbstractions;

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