using System;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class HomeTaskFactory : IHomeTaskFactory
    {        
        public async Task<Entities.HomeTask> CreateAsync(HomeTask dataModel)
        {
            if (dataModel == null)
                throw new ArgumentNullException(nameof(dataModel));

            try
            {
                var startDate = DateOnly.FromDateTime(dataModel.StartDate);
                var endDate = DateOnly.FromDateTime(dataModel.EndDate);

                var homeTaskName = new HomeTaskName(dataModel.Title);
                var duration = new Duration(startDate, endDate);
                
                Domain.ValueObjects.File? material = null;                

                return new Entities.HomeTask(
                    id: dataModel.Id,
                    homeTaskName: homeTaskName,
                    description: dataModel.Description,
                    duration: duration,
                    material: material);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Ошибка при создании HomeTask из data model (ID: {dataModel.Id})", ex);
            }
        }

        public async Task<HomeTask> CreateDataModelAsync(Entities.HomeTask domainEntity)
        {
            if (domainEntity == null)
                throw new ArgumentNullException(nameof(domainEntity));

            try
            {
                var materialPath = domainEntity.Material?.GetFullPath();

                return new HomeTask
                {
                    Id = domainEntity.Id,
                    //LessonId = domainEntity.LessonId,
                    Title = domainEntity.HomeTaskName.Value,
                    Description = domainEntity.Description,
                    StartDate = domainEntity.Duration.StartDate.ToDateTime(TimeOnly.MinValue),
                    EndDate = domainEntity.Duration.EndDate.ToDateTime(TimeOnly.MinValue),
                    Material = materialPath
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Ошибка при создании data model из HomeTask (ID: {domainEntity.Id})", ex);
            }
        }
    }
}