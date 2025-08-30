using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class HomeWorkFactory : IHomeWorkFactory //IBaseFactory<Common.Domain.Entities.HomeWork, HomeWork>
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

            var material = dataModel.Material != null ? _fileFactory.Create(dataModel.Material) : null;
            var score = new Score(dataModel.Score);
            var status = dataModel.Score < 40 ? HomeworkStatus.Rejected : HomeworkStatus.Submitted;

            // TODO: Нужно получить deadline из HomeTask для проверки IsOnTime
            var isOnTime = true; // Временное значение, нужно реализовать логику проверки дедлайна

            var taskCompletionDate = new TaskCompletionDate(dataModel.TaskCompletionDate);

            return new Common.Domain.Entities.HomeWork(
                dataModel.Id,
                dataModel.HomeTaskId,
                dataModel.StudentId,
                score,
                taskCompletionDate,
                material,
                dataModel.StudentComment,
                dataModel.TeacherComment,
                status,
                isOnTime
            );
            
        }

        public Task<HomeWork> CreateDataModelAsync(Common.Domain.Entities.HomeWork domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            var materialPath = domainEntity.Material != null ?
                _fileFactory.GetFullPath(domainEntity.Material) : null;

            var taskCompletionDateTime = domainEntity.TaskCompletionDate.Value.ToDateTime(TimeOnly.MinValue);

            return Task.FromResult(new HomeWork
            {
                Id = domainEntity.Id,
                HomeTaskId = domainEntity.HomeTaskId,
                StudentId = domainEntity.StudentId,
                Score = domainEntity.Score.Value,
                TaskCompletionDate = taskCompletionDateTime,
                Material = materialPath,
                StudentComment = domainEntity.StudentComment,
                TeacherComment = domainEntity.TeacherComment
            });
        }
    }
}