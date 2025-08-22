// Infrastructure/Factories/HomeWorkFactory.cs
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class HomeWorkFactory : IBaseFactory<Common.Domain.Entities.HomeWork, HomeWork>
    {
        private readonly IFileFactory _fileFactory;

        public HomeWorkFactory(IFileFactory fileFactory) : base()
        {
            _fileFactory = fileFactory ?? throw new ArgumentNullException(nameof(fileFactory));
        }

        public async Task<Common.Domain.Entities.HomeWork> CreateAsync(HomeWork dataModel)
        {
            if (dataModel is null)
                throw new ArgumentNullException(nameof(dataModel));
            try
            {
                var material = _fileFactory.Create(dataModel.Material);
                var status = dataModel.Score < 40 ? HomeworkStatus.Rejected : HomeworkStatus.Submitted;
                var isOnTime = DateTime.Now.Date <= dataModel.TaskCompletionDate.Date; // TODO: Change this to check if it's submitted before the deadline
                var taskCompletionDate = new TaskCompletionDate(dataModel.TaskCompletionDate);
                return new Common.Domain.Entities.HomeWork(
                    dataModel.Id,
                    dataModel.StudentId,
                    dataModel.StudentComment,
                    taskCompletionDate,
                    status, isOnTime
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating HomeWork (ID: {dataModel.Id})", ex);
            }
        }

        public Task<HomeWork> CreateDataModelAsync(Common.Domain.Entities.HomeWork domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            var materialPath = _fileFactory.GetFullPath(domainEntity.Material);

            return Task.FromResult(new HomeWork
            {
                // Id = domainEntity.Id,
                // Title = domainEntity.HomeWorkName.Value,
                // Description = domainEntity.Description,
                // StartDate = domainEntity.Duration.StartDate.ToDateTime(TimeOnly.MinValue),
                // EndDate = domainEntity.Duration.EndDate.ToDateTime(TimeOnly.MinValue),
                // Material = materialPath
            });
        }
    }
}