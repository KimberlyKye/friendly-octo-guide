using Application.Models.Teacher;

namespace Application.Services.Abstractions
{
    /// <summary>
    /// Сервис для работы с профилем пользователя (в т.ч. CRUD операции)
    /// </summary>
    public interface ITeacherProfileService
    {
        /// <summary>
        /// Создание профиля
        /// </summary>
        /// <param name="profile">Учётные данные профиля</param>
        /// <returns>Созданный профиль</returns>
        Task<TeacherProfileModel> CreateProfileAsync(CreateTeacherModel profileInfo);

        /// <summary>
        /// Получение профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns>Запрашиваемый профиль</returns>
        Task<TeacherProfileModel> GetProfileInfoAsync(long profileId);

        /// <summary>
        /// Обновить информацию в профиле пользователя
        /// </summary>
        /// <param name="profileInfo">Информация для изменения</param>
        /// <returns>Измененный профиль</returns>
        Task<TeacherProfileModel> UpdateProfileInfoAsync(TeacherProfileModel profileInfo);

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns></returns>
        Task DeleteProfileAsync(long profileId);
    }
}
