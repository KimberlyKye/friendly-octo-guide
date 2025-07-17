using Domain.ValueObjects;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using System;
using System.IO;

namespace Infrastructure.Factories
{
    public class FileFactory : IFileFactory
    {
        public Domain.ValueObjects.File? Create(string fullPath)
        {
            if (fullPath is null) { return null; };

            var dirPath = Path.GetDirectoryName(fullPath);
            var fileName = Path.GetFileNameWithoutExtension(fullPath);
            var extension = Path.GetExtension(fullPath)?.TrimStart('.');

            return new Domain.ValueObjects.File(dirPath, fileName, extension);            
        }

        public string? GetFullPath(Domain.ValueObjects.File? file)
        {
            if (file is null) { return null; };

            return file?.GetFullPath();
        }
    }
}