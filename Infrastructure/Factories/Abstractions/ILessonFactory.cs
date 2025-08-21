
namespace Infrastructure.Factories.Abstractions
{
    public interface ILessonFactory
    {
        Task<Entities.Lesson> CreateAsync(DataModels.Lesson dataModel, DataModels.HomeTask? homeTask = null);
        Task<DataModels.Lesson> CreateDataModelAsync(Entities.Lesson domainEntity);
    }
}