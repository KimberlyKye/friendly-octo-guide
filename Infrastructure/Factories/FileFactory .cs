using Domain.ValueObjects;
using Infrastructure.Factories.Abstractions;
using System;
using System.IO;

namespace Infrastructure.Factories
{
    public class FileFactory : IFileFactory
    {
        public Domain.ValueObjects.File? Create(string? fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                return null;

            try
            {
                var dirPath = Path.GetDirectoryName(fullPath);
                var fileName = Path.GetFileNameWithoutExtension(fullPath);
                var extension = Path.GetExtension(fullPath)?.TrimStart('.');

                if (string.IsNullOrWhiteSpace(dirPath) ||
                    string.IsNullOrWhiteSpace(fileName) ||
                    string.IsNullOrWhiteSpace(extension))
                {
                    throw new ArgumentException("Invalid file path structure");
                }

                return new Domain.ValueObjects.File(dirPath, fileName, extension);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create File object", ex);
            }
        }

        public string? GetFullPath(Domain.ValueObjects.File? file)
        {
            return file?.GetFullPath();
        }
    }
}