namespace Application.Services
{
    /// <summary>
    /// Тип для хранения настроек файлового хранилища.
    /// </summary>
    public class FileStorageOptions
    {
        /// <summary>
        /// Путь к корневой папке хранилища.
        /// </summary>
        /// <value></value>
        public string RootPath { get; set; }
    }
}