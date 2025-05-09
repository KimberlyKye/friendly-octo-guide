
namespace Infrastructure.Factories.Abstractions
{
    public interface ILessonFactory
    {
        Task<Entities.Lesson> CreateAsync(DataModels.Lesson dataModel, IEnumerable<DataModels.HomeTask>? homeTasks = null);
        Task<DataModels.Lesson> CreateDataModelAsync(Entities.Lesson domainEntity);
    }
}