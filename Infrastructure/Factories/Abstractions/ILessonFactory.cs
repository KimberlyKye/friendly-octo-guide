namespace Infrastructure.Factories.Abstractions
{
    public interface ILessonFactory
    {
        Task<Common.Domain.Entities.Lesson> CreateAsync(DataModels.Lesson dataModel, DataModels.HomeTask? homeTask = null);
        Task<DataModels.Lesson> CreateDataModelAsync(Common.Domain.Entities.Lesson domainEntity);
    }
}