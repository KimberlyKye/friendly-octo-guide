namespace Infrastructure.Factories.Abstractions
{
    public interface IHomeTaskFactory
    {
        Task<Entities.HomeTask> CreateAsync(DataModels.HomeTask dataModel);
        Task<DataModels.HomeTask> CreateDataModelAsync(Entities.HomeTask domainEntity);
    }
}