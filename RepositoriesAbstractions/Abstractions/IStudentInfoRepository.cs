using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentInfoRepository
    {
        Task<Student?> GetStudentById(int studentId);

        Task<List<Course>> GetAllCourses(int studentId);

        Task<Course?> GetCourseInfo(int courseId, int studentId);

        Task<Entities.Course> GetAllCourseInfo(int courseId, int studentId);

        /// <summary>
        /// Получение списка студентов курса
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<List<Student>> GetAllStudentsByCourse(int courseId);

        /// <summary>
        /// Получение списка студентов, не учащихся на выбранном курсе (с пагинацией)
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
        Task AddStudentsToCourse(int courseId, int[] studentIds);

        /// <summary>
        /// Удаление студентов из курса
        /// </summary>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        Task RemoveStudentsFromCourse(int courseId, int[] studentIds);

        /// <summary>
        ///  Проверка на наличие студента в курсе
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        Task<bool> CheckIfUserInCourse(int userId, int courseId);
    }
}
