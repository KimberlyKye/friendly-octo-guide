using Application.Models.Student;

namespace Application.Services.Abstractions
{
    /// <summary>
    /// Сервис для работы с профилем пользователя (в т.ч. CRUD операции)
    /// </summary>
    public interface IStudentProfileService
    {
        /// <summary>
        /// Создание профиля
        /// </summary>
        /// <param name="profile">Учётные данные профиля</param>
        /// <returns>Созданный профиль</returns>
        Task<StudentProfileModel> CreateProfileAsync(CreateStudentModel profileInfo);

        /// <summary>
        /// Получение профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns>Запрашиваемый профиль</returns>
        Task<StudentProfileModel> GetProfileInfoAsync(long profileId);

        /// <summary>
        /// Обновить информацию в профиле пользователя
        /// </summary>
        /// <param name="profileInfo">Информация для изменения</param>
        /// <returns>Измененный профиль</returns>
        Task<StudentProfileModel> UpdateProfileInfoAsync(StudentProfileModel profileInfo);

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns></returns>
        Task DeleteProfileAsync(long profileId);
    }
}
