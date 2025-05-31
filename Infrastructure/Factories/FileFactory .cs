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
            
            var dirPath = Path.GetDirectoryName(fullPath);
            var fileName = Path.GetFileNameWithoutExtension(fullPath);
            var extension = Path.GetExtension(fullPath)?.TrimStart('.');

            return new Domain.ValueObjects.File(dirPath, fileName, extension);            
        }

        public string? GetFullPath(Domain.ValueObjects.File? file)
        {
            return file?.GetFullPath();
        }
    }
}