// Infrastructure/Factories/HomeWorkFactory.cs
using System;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using ValueObjects;
using ValueObjects.Enums;

namespace Infrastructure.Factories
{
    public class HomeWorkFactory : IBaseFactory<Entities.HomeWork, HomeWork>
    {
        private readonly IFileFactory _fileFactory;

        public HomeWorkFactory(IFileFactory fileFactory) : base()
        {
            _fileFactory = fileFactory ?? throw new ArgumentNullException(nameof(fileFactory));
        }

        public async Task<Entities.HomeWork> CreateAsync(HomeWork dataModel)
        {
            if (dataModel is null)
                throw new ArgumentNullException(nameof(dataModel));
            try
            {
                var material = _fileFactory.Create(dataModel.Material);
                var status = dataModel.Score < 40 ? HomeworkStatus.Rejected : HomeworkStatus.Submitted;
                var isOnTime = DateTime.Now.Date <= dataModel.TaskCompletionDate.Date; // TODO: Change this to check if it's submitted before the deadline
                var taskCompletionDate = new TaskCompletionDate(dataModel.TaskCompletionDate);
                return new Entities.HomeWork(
                    dataModel.HomeTaskId,
                    dataModel.StudentId,
                    dataModel.StudentComment,
                    taskCompletionDate,
                    status,
                    isOnTime
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating HomeWork (ID: {dataModel.Id})", ex);
            }
        }

        public Task<HomeWork> CreateDataModelAsync(Entities.HomeWork domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            var materialPath = _fileFactory.GetFullPath(domainEntity.Material);

            return Task.FromResult(new HomeWork
            {
                Id = domainEntity.Id,
                StudentId = domainEntity.StudentId,
                StudentComment = domainEntity.StudentComment,
                TaskCompletionDate = domainEntity.TaskCompletionDate.Value.ToDateTime(TimeOnly.MinValue),
                Score = domainEntity.Score,
                Material = materialPath
            });
        }

    }
}