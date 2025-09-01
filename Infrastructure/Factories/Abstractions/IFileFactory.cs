namespace Infrastructure.Factories.Abstractions
{
    public interface IFileFactory
    {
        Common.Domain.ValueObjects.File? Create(string? fullPath);
        string? GetFullPath(Common.Domain.ValueObjects.File? file);
    }
}
