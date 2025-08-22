using Common.Domain.Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentInfoRepository
    {
        Task<Student?> GetStudentById(int studentId);
        Task<List<Course>> GetAllCourses(int studentId);
        Task<Course?> GetCourseInfo(int courseId, int studentId);
        Task<Course?> GetAllCourseInfo(int courseId, int studentId);
        Task<Lesson?> GetHomeworksInfo(int lessonId, int studentId);
    }
}
