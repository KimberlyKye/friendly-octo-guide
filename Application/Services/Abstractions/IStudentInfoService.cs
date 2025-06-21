using Application.Models.Course;
using Application.Models.Teacher.Responses;
using Entities;


namespace Application.Services.Abstractions
{
    public interface IStudentInfoService
    {
        Task <List<StudentAllCoursesModel>> GetAllCourses(int studentId);
        Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId);
        Task<List<Lesson?>> GetLessonsInfoByCourse(int courseId, int studentId);
    }
}
