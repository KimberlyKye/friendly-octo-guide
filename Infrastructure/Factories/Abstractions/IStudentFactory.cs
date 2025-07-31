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
        public Task<Student> CreateFromAsync(User userModel);

        /// <summary>
        /// Создает студента (Domain) из модели пользователя (DataModel)
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public Student CreateFrom(User userModel);

        /// <summary>
        /// Создает модель пользователя (DataModel) из студента (Domain)
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Task<User> CreateDataModelAsync(Student student);

    }
}