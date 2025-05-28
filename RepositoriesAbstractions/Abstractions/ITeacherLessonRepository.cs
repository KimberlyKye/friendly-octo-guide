using Entities;

namespace RepositoriesAbstractions.Abstractions
{
    public interface ITeacherLessonRepository
    {
        Task<int> AddLesson(Lesson lesson);
    }
}
