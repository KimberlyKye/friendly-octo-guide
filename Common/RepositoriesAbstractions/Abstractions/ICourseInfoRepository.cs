namespace Common.RepositoriesAbstractions.Abstractions
{
    public interface ICourseInfoRepository
    {
        Task<bool> CheckIsCourseExistAndActiveById(int courseId);
        Task<bool> IsCourseOwnedByTeacherAsync(int courseId, int teacherId);
    }
}
