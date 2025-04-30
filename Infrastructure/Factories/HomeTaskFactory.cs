// Infrastructure/Factories/HomeTaskFactory.cs
using System;
using System.Threading.Tasks;
using Domain.ValueObjects;
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

        public async Task<Entities.HomeTask> CreateAsync(HomeTask dataModel)
        {
            if (dataModel == null)
                throw new ArgumentNullException(nameof(dataModel));

            try
            {
                var startDate = DateOnly.FromDateTime(dataModel.StartDate);
                var endDate = DateOnly.FromDateTime(dataModel.EndDate);
                var homeTaskName = new HomeTaskName(dataModel.Title);
                var material = _fileFactory.Create(dataModel.Material);

                return new Entities.HomeTask(
                    id: dataModel.Id,
                    homeTaskName: homeTaskName,
                    description: dataModel.Description,
                    duration: new Duration(startDate, endDate),
                    material: material);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating HomeTask (ID: {dataModel.Id})", ex);
            }
        }

        public Task<HomeTask> CreateDataModelAsync(Entities.HomeTask domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

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