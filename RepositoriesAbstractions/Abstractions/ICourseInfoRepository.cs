
namespace RepositoriesAbstractions.Abstractions
{
    public interface ICourseInfoRepository
    {
        Task<bool> CheckIsCourseExistAndActiveById(int courseId);
    }
}
