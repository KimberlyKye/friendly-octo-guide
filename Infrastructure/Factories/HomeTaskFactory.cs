using Common.Domain.ValueObjects;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class HomeTaskFactory : IHomeTaskFactory
    {
        private readonly IFileFactory _fileFactory;

        public HomeTaskFactory(IFileFactory fileFactory)
        {
            _fileFactory = fileFactory ?? throw new ArgumentNullException(nameof(fileFactory));
        }

        public async Task<Common.Domain.Entities.HomeTask> CreateAsync(HomeTask dataModel)
        {
            var startDate = DateOnly.FromDateTime(dataModel.StartDate);
            var endDate = DateOnly.FromDateTime(dataModel.EndDate);
            var homeTaskName = new HomeTaskName(dataModel.Title);
            var material = _fileFactory.Create(dataModel.Material);

            return new Common.Domain.Entities.HomeTask(
                id: dataModel.Id,
                homeTaskName: homeTaskName,
                description: dataModel.Description,
                duration: new Duration(startDate, endDate),
                material: material);
        }

        public Task<HomeTask> CreateDataModelAsync(Common.Domain.Entities.HomeTask domainEntity)
        {
            var materialPath = _fileFactory.GetFullPath(domainEntity.Material);

            return Task.FromResult(new HomeTask
            {
                Id = domainEntity.Id,
                Title = domainEntity.HomeTaskName.Value,
                Description = domainEntity.Description,
                StartDate = domainEntity.Duration.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = domainEntity.Duration.EndDate.ToDateTime(TimeOnly.MinValue),
                Material = materialPath
            });
        }
    }
}