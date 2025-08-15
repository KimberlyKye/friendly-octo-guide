using Application.Models.Course;
using Application.Models.Lesson;
using Application.Models.Teacher.Responses;
using Entities;


namespace Application.Services.Abstractions
{
    public interface IStudentInfoService
    {
        Task <List<StudentAllCoursesModel>> GetAllCourses(int studentId);
        Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId);
        Task<List<LessonInfoByCourseModel>?> GetLessonsInfoByCourse(int courseId, int studentId);
        Task<List<LessonInfoByCourseModel>?> GetLessonAndHomeworkInfo(int lessonId, int studentId);
        
    }
}
