using Entities;
using Infrastructure.DataModels;

namespace Infrastructure.Factories.Abstractions
{
    public interface IStudentFactory
    {
        /// <summary>
        /// Создает студента (Domain) из модели пользователя (DataModel)
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public Student CreateFrom(User userModel);
    }
}