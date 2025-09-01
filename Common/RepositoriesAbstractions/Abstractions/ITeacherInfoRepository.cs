using Common.Domain.Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface ITeacherInfoRepository
    {
        Task<Teacher?> GetTeacherById(int teacherId);
    }
}
