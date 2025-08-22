using Common.Domain.ValueObjects;
using Infrastructure.Factories.Abstractions;

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

        public async Task<Common.Domain.Entities.Lesson> CreateAsync(DataModels.Lesson lesson, DataModels.HomeTask? homeTask = null)
        {
            var material = _fileFactory.Create(lesson.Material);
            var domainHomeTask = homeTask != null
                ? await _homeTaskFactory.CreateAsync(homeTask)
                : null;
            Score? pScore = lesson.Score != 0
                ? new Score(lesson.Score)
                : null;

            return new Common.Domain.Entities.Lesson(
                id: lesson.Id,
                courseId: lesson.CourseId,
                lessonName: new LessonName(lesson.Title),
                description: lesson.Description,
                date: lesson.Date,
                material: material,
                homeTask: domainHomeTask,
                score: pScore);
        }
        public Task<DataModels.Lesson> CreateDataModelAsync(Common.Domain.Entities.Lesson domainEntity)
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