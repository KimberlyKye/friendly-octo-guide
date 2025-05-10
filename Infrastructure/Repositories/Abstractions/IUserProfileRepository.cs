namespace Infrastructure.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий для действий с профилем человека
    /// </summary>
    public interface IUserProfileRepository<T>
    {
        /// <summary>
        /// Метод создания человека
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<T> CreateUserProfileAsync(T user);

        /// <summary>
        /// Метод обновления профиля человека
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<T> UpdateUserProfileAsync(T user);

        /// <summary>
        /// Метод получения человека
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<T> GetUserProfileAsync(long userId);

        /// <summary>
        /// Метод проверки наличия пользователя с данным адресом электронной почты
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> CheckUserByEmail(string email);
    }
}
