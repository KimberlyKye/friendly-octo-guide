// Infrastructure/Factories/LessonFactory.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using File = Domain.ValueObjects.File;

namespace Infrastructure.Factories
{
    public class LessonFactory : ILessonFactory
    {
        private readonly IHomeTaskFactory _homeTaskFactory;
        private readonly IFileFactory _fileFactory;

        public LessonFactory(
            IHomeTaskFactory homeTaskFactory,
            IFileFactory fileFactory)
        {
            _homeTaskFactory = homeTaskFactory ?? throw new ArgumentNullException(nameof(homeTaskFactory));
            _fileFactory = fileFactory ?? throw new ArgumentNullException(nameof(fileFactory));
        }

        public async Task<Entities.Lesson> CreateAsync(DataModels.Lesson dataModel, IEnumerable<DataModels.HomeTask>? homeTasks = null)
        {
            if (dataModel is null)
                throw new ArgumentNullException(nameof(dataModel));

            var material = _fileFactory.Create(dataModel.Material);
            var domainHomeTasks = homeTasks != null
                ? await CreateHomeTasksAsync(homeTasks)
                : null;

            return new Entities.Lesson(
                id: dataModel.Id,
                courseId : dataModel.CourseId,
                lessonName: new LessonName(dataModel.Title),
                description: dataModel.Description,
                date: dataModel.Date,
                material: material,
                homeTasks: domainHomeTasks);
        }

        public Task<DataModels.Lesson> CreateDataModelAsync(Entities.Lesson domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            var materialPath = _fileFactory.GetFullPath(domainEntity.Material);

            return Task.FromResult(new DataModels.Lesson
            {
                Id = domainEntity.Id,
                CourseId = domainEntity.CourseId,
                Title = domainEntity.Name.Value,
                Description = domainEntity.Description,
                Date = domainEntity.Date,
                Material = materialPath
            });
        }

        private async Task<List<Entities.HomeTask>> CreateHomeTasksAsync(IEnumerable<DataModels.HomeTask> homeTasks)
        {
            var tasks = homeTasks
                .Where(h => h != null)
                .Select(ht => _homeTaskFactory.CreateAsync(ht));

            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}