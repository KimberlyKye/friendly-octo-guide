using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    /// <summary>
    /// Репозиторий для студента.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Получение списка ID курсов студента.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetCourseIdsForStudentAsync(int studentId);


        /// <summary>
        /// Получение списка занятий для отображения в календаре студента на выбранный период.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Entities.Lesson>> GetLessonsByDateRangeAndStudentAsync(int studentId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Получение списка домашних заданий для отображения в календаре студента на выбранный период.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<Entities.HomeTask>> GetHomeTasksByDeadlineRangeAndStudentAsync(int studentId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Получение всего календаря студента на выбранный период.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="monthStart"></param>
        /// <param name="monthEnd"></param>
        /// <returns></returns>
        Task<(List<Entities.Lesson> Lessons, List<HomeTask> HomeTasks)> GetMonthlyDataForStudentAsync(int studentId, DateTime monthStart, DateTime monthEnd);

    }
}