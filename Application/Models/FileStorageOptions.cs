using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
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