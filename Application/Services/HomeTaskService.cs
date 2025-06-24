using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Entities;

namespace Application.Services
{
    public class HomeTaskService : IHomeTaskService
    {
        public Task<HomeTask> GetByIdAsync(int homeTaskId)
        {
            return new Task<HomeTask>(null);
        }
    }
}