namespace Infrastructure.Factories.Abstractions
{
    public interface IHomeWorkFactory
    {
        /// <summary>
        /// Создает создаёт домашнюю работу (Domain) из модели базы данных (DataModel)
        /// </summary>
        /// <param name="homeWorkModel"></param>
        /// <returns></returns>
        public Task<Entities.HomeWork> CreateFromAsync(DataModels.HomeWork homeWorkModel);

        /// <summary>
        /// Создает модель базы данных (DataModel) из домашней работы (Domain)
        /// </summary>
        /// <param name="homeWork"></param>
        /// <returns></returns>
        public Task<DataModels.HomeWork> CreateDataModelAsync(Entities.HomeWork homeWork);
    }
}
