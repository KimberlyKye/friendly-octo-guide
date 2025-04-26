using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Exceptions.Types
{
    public class ApiErrorResponse
    {
        /// <summary>
        /// Код ошибки. Может быть кодом HTTP-статуса или иным кодом ошибки
        /// </summary>
        /// <value></value>
        public int Code { get; set; }

        /// <summary>
        /// Название ошибки. Например, 400 Bad Request, 500 Internal Server Error. В случае неизвестной ошибки - "Unknown error"
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Текстовое описание ошибки. В случае неизвестной ошибки - "Unknown error"
        /// </summary>
        /// <value></value>
        public string MessageHumanReadable { get; set; }
        /// <summary>
        ///  Текстовое описание ошибки для технических целей. Может быть полезно для анализа ошибок. Например, в случае ошибки при чтении файла - имя файла, размер файла, путь к файлу, код ошибки и т.д. В случае неизвестной ошибки - "Unknown error"
        /// </summary>
        /// <value></value>
        public string MessageTechnical { get; set; }

        /// <summary>
        ///  Контекст ошибки. Например, поле или действие, которое вызвало ошибку. В случае неизвестной ошибки - пустая строка
        /// </summary>
        /// <value></value>
        public string StackTrace { get; set; }
    }
}