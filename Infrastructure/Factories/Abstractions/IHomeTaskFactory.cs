namespace Infrastructure.Factories.Abstractions
{
    public interface IHomeTaskFactory
    {
        Task<Common.Domain.Entities.HomeTask> CreateAsync(DataModels.HomeTask dataModel);
        Task<DataModels.HomeTask> CreateDataModelAsync(Common.Domain.Entities.HomeTask domainEntity);
    }
}