using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Abstractions
{
    public interface IHomeTaskFactory
    {
        Task<Entities.HomeTask> CreateAsync(DataModels.HomeTask dataModel);
        Task<DataModels.HomeTask> CreateDataModelAsync(Entities.HomeTask domainEntity);
    }
}