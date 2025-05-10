using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Abstractions
{
    public interface IFileFactory
    {
        Domain.ValueObjects.File? Create(string? fullPath);
        string? GetFullPath(Domain.ValueObjects.File? file);
    }
}
