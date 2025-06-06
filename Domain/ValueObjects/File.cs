﻿using Domain.ValueObjects.Base;

namespace Domain.ValueObjects
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
        /// Возвращает полный путь к файлу
        /// </summary>
        public string GetFullPath() => System.IO.Path.Combine(Path, $"{Name}.{Extension}");

        /// <summary>
        /// Возвращает строковое представление файла (полный путь)
        /// </summary>
        public override string ToString() => GetFullPath();
    }
}