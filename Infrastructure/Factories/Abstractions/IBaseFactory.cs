using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Abstractions
{
    /// <summary>
    /// Общая фабрика для однотипных преобразований между доменной моделью и моделью БД
    /// </summary>
    /// <typeparam name="T1">Тип доменной модели</typeparam>
    /// <typeparam name="T2">Тим модели БД</typeparam>
    public interface IBaseFactory<T1, T2>
    {
        /// <summary>
        /// Сделать из модели БД - доменную модель 
        /// </summary>
        /// <param name="dataModel">Модель БД</param>
        /// <returns>Доменная модель</returns>
        Task<T1> CreateAsync(T2 dataModel);

        /// <summary>
        /// Сделать из доменной модели - модель БД
        /// </summary>
        /// <param name="domainEntity">Доменная модель</param>
        /// <returns>Модель БД</returns>
        Task<T2> CreateDataModelAsync(T1 domainEntity);
    }
}