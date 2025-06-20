using Application.Models.Teacher.Responses;
using Entities;


namespace Application.Services.Abstractions
{
    public interface IStudentInfoService
    {
        Task <List<StudentAllCoursesModel>> GetAllCourses(int studentId);
        Task<Course?> GetCourseInfo(int courseId, int studentId);
        Task<List<Lesson?>> GetLessonsInfoByCourse(int courseId, int studentId);
    }
}
