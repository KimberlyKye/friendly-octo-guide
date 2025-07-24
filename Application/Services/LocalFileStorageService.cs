using Microsoft.Extensions.Options;
using Application.Models;

namespace Application.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _storagePath;

    public LocalFileStorageService(IOptions<FileStorageOptions> options)
    {
        _storagePath = options.Value.RootPath;
    
        Directory.CreateDirectory(_storagePath);
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
    {
        var fileId = Guid.NewGuid();
        var fileExtension = Path.GetExtension(fileName);
        var storedFileName = $"{fileId}{fileExtension}";
        var filePath = Path.Combine(_storagePath, storedFileName);

        await using var output = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(output);

        return filePath;
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        return Task.FromResult<Stream>(File.OpenRead(filePath));
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }
}
