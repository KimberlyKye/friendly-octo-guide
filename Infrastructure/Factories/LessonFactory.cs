using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using File = Domain.ValueObjects.File;

namespace Infrastructure.Factories
{
    public class LessonFactory : ILessonFactory
    {
        private readonly IHomeTaskFactory _homeTaskFactory;

        public LessonFactory(IHomeTaskFactory homeTaskFactory)
        {
            _homeTaskFactory = homeTaskFactory ?? throw new ArgumentNullException(nameof(homeTaskFactory));
        }

        public async Task<Entities.Lesson> CreateAsync(Lesson dataModel, IEnumerable<HomeTask>? homeTasks = null)
        {
            if (dataModel == null)
                throw new ArgumentNullException(nameof(dataModel));

            // Создание объекта File из полного пути Material
            File? material = null;            

            // Асинхронное создание HomeTasks
            var domainHomeTasks = homeTasks != null
                ? await CreateHomeTasksAsync(homeTasks)
                : null;

            return new Entities.Lesson(
                id: dataModel.Id,
                lessonName: new LessonName(dataModel.Title),
                description: dataModel.Description,
                date: dataModel.Date,
                material: material,
                homeTasks: domainHomeTasks);
        }

        public Task<Lesson> CreateDataModelAsync(Entities.Lesson domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            var materialPath = domainEntity.Material?.GetFullPath();

            return Task.FromResult(new Lesson
            {
                Id = domainEntity.Id,
                //CourseId = domainEntity.CourseId,
                Title = domainEntity.Name.Value,
                Description = domainEntity.Description,
                Date = domainEntity.Date,
                Material = materialPath
            });
        }

        private async Task<List<Entities.HomeTask>> CreateHomeTasksAsync(IEnumerable<HomeTask> homeTasks)
        {
            var result = new List<Entities.HomeTask>();
            foreach (var ht in homeTasks.Where(h => h != null))
            {
                result.Add(await _homeTaskFactory.CreateAsync(ht));
            }
            return result;
        }        
    }
}