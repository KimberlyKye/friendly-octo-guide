using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    /// <summary>
    /// Репозиторий для курса
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Добавить курс
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Task<int> AddCourseAsync(Course course);

        /// <summary>
        /// Получить курс по id.
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Task<Course> GetCourseAsync(int courseId);

        /// <summary>
        /// Получить все курсы студента.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Task<Course[]> GetAllStudentCoursesAsync(Student student);

        /// <summary>
        /// Получить все курсы преподавателя.
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public Task<Course[]> GetAllTeacherCoursesAsync(Teacher teacher);

        /// <summary>
        /// Обновить курс.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public Task<int> UpdateCourseAsync(Course course);

        /// <summary>
        /// Получить урок по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public Task<Lesson> GetLessonByIdAsync(int id);

        /// <summary>
        /// Обновить урок.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public Task<int> UpdateLesson(Lesson lesson);
    }
}