using Application.Models.Course;
using Application.Models.Lesson;
using Application.Models.Teacher.Responses;
using Entities;


namespace Application.Services.Abstractions
{
    public interface IStudentInfoService
    {
        Task<List<StudentAllCoursesModel>> GetAllCourses(int studentId);

        Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId);

        Task<List<LessonInfoByCourseModel>?> GetLessonsInfoByCourse(int courseId, int studentId);

        /// <summary>
        /// Получение списка всех студентов на курсе
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<List<Student>> GetAllStudentsByCourse(int courseId);

        /// <summary>
        /// получение списка студентов вне курса
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns></returns>
        Task<List<Student>> GetAllStudentsOutsideCourse(int courseId, int startRow, int endRow);

        /// <summary>
        /// Добавление студентов на курс
        /// </summary>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> AddStudentsToCourse(int courseId, int[] studentIds);

        /// <summary>
        /// Удаление студентов с курса
        /// </summary>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> RemoveStudentsFromCourse(int courseId, int[] studentIds);
    }
}
