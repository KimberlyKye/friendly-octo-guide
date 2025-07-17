using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Application.Services.Abstractions
{
    public interface IHomeTaskService
    {
        Task<HomeTask> GetByIdAsync(int homeTaskId);
    }
}