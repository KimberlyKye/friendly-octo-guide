namespace Application.Services.Abstractions
{
    /// <summary>
    /// Сервис для работы с профилем пользователя (в т.ч. CRUD операции)
    /// </summary>
    public interface IProfileService<T>
    {
        /// <summary>
        /// Создание профиля
        /// </summary>
        /// <param name="profile">Учётные данные профиля</param>
        /// <returns>Созданный профиль</returns>
        Task<T> CreateProfileAsync(T profileInfo);

        /// <summary>
        /// Получение профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns>Запрашиваемый профиль</returns>
        Task<T> GetProfileInfoAsync(long profileId);

        /// <summary>
        /// Обновить информацию в профиле пользователя
        /// </summary>
        /// <param name="profileInfo">Информация для изменения</param>
        /// <returns>Измененный профиль</returns>
        Task<T> UpdateProfileInfoAsync(T profileInfo);

        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="profileId">ID пользователя</param>
        /// <returns></returns>
        Task DeleteProfileAsync(long profileId);
    }
}
