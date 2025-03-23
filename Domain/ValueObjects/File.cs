using Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValueObjects
{
    /// <summary>
    /// Название курса.
    /// </summary>
    public class File : IValueObject
    {
        public string _path;
        public string _extension;
        public string _name;
        public File(string path, string name, string extension)
        {
            _path = path;
            _name = name;
            _extension = extension;
        }
    }
}
