
using Common.Domain.Entities;

namespace Common.RepositoriesAbstractions;

public interface IHomeWorkRepository
{
    /// <summary>
    /// Производит отправку домашнего задания.
    /// </summary>
    /// <param name="submission"></param>
    /// <returns></returns>
    Task<HomeWork> SubmitHomeworkAsync(HomeWork submission);

    /// <summary>
    /// Получить задание.
    /// </summary>
    /// <param name="submissionId"></param>
    /// <returns></returns>
    Task<HomeWork?> GetSubmissionAsync(int submissionId);

    /// <summary>
    /// Получить задание для студента.
    /// </summary>
    /// <param name="submissionId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    Task<HomeWork?> GetSubmissionForStudentAsync(int submissionId, int studentId);

    /// <summary>
    /// Сдать задание.
    /// </summary>
    /// <param name="submission"></param>
    /// <returns></returns>
    Task<HomeWork> UpdateSubmissionAsync(HomeWork submission);

    /// <summary>
    /// Проверить, есть ли неоцененное задание.
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="homeTaskId"></param>
    /// <returns></returns>
    Task<bool> HasUngradedSubmissionAsync(int studentId, int homeTaskId);

    /// <summary>
    /// Проверить, оценено ли задание.
    /// </summary>
    /// <param name="studentId"></param>
    /// <param name="homeTaskId"></param>
    /// <returns></returns>
    Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId);
}