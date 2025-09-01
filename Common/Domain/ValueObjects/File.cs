using Common.Domain.ValueObjects.Base;

namespace Common.Domain.ValueObjects
{
    /// <summary>
    /// Значение, представляющее файл в системе.
    /// Содержит информацию о пути к файлу, его имени и расширении.
    /// </summary>
    public class File : IValueObject
    {
        /// <summary>
        /// Полный путь к файлу
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Расширение файла (без точки)
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Имя файла (без расширения)
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// Создает новый экземпляр класса File
        /// </summary>
        /// <param name="path">Путь к директории, содержащей файл</param>
        /// <param name="name">Имя файла без расширения</param>
        /// <param name="extension">Расширение файла без точки</param>
        /// <exception cref="ArgumentNullException">Если любой из параметров null или пуст</exception>
        public File(string path, string name, string extension)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "Путь к файлу не может быть пустым");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Имя файла не может быть пустым");

            if (string.IsNullOrWhiteSpace(extension))
                throw new ArgumentNullException(nameof(extension), "Расширение файла не может быть пустым");

            Path = path;
            Name = name;
            Extension = extension;
        }

        /// <summary>
        /// Создает новый экземпляр класса File на основе полного пути к файлу
        /// </summary>
        /// <param name="filePath">Полный путь к файлу</param>
        public File(string filePath)
        {
            var arr1 = filePath.Split('/');
            Path = string.Join("/", arr1.Take(arr1.Length - 1));

            var arr2 = arr1[arr1.Length - 1].Split('.');
            Name = string.Join(".", arr2.Take(arr2.Length - 1));
            Extension = arr2[arr2.Length - 1];
        }


        /// <summary>
        /// Возвращает полный путь к файлу
        /// </summary>
        public string GetFullPath() => System.IO.Path.Combine(Path, $"{Name}.{Extension}");

        /// <summary>
        /// Возвращает строковое представление файла (полный путь)
        /// </summary>
        public override string ToString() => GetFullPath();
    }
}