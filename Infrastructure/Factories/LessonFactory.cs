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

        public async Task<Entities.Lesson> CreateAsync(DataModels.Lesson dataModel, DataModels.HomeTask? homeTask = null)
        {
            var material = _fileFactory.Create(dataModel.Material);
            var domainHomeTask = homeTask != null
                ? await _homeTaskFactory.CreateAsync(homeTask)
                : null;
            Score? pScore = dataModel.Score != 0
                ? new Score(dataModel.Score)
                : null;

            return new Entities.Lesson(
                id: dataModel.Id,
                courseId : dataModel.CourseId,
                lessonName: new LessonName(dataModel.Title),
                description: dataModel.Description,
                date: dataModel.Date,
                material: material,
                homeTask: domainHomeTask,
                score: pScore);
        }
        public Task<DataModels.Lesson> CreateDataModelAsync(Entities.Lesson domainEntity)
        {
            var materialPath = _fileFactory.GetFullPath(domainEntity.Material);

            return Task.FromResult(new DataModels.Lesson
            {
                Id = domainEntity.Id,
                CourseId = domainEntity.CourseId,
                Title = domainEntity.Name.Value,
                Description = domainEntity.Description,
                Date = domainEntity.Date,
                Material = materialPath,
                Score = (int)domainEntity.Score
            });
        }
    }
}