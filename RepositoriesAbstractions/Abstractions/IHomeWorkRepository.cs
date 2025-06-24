
using Entities;

namespace RepositoriesAbstractions;

public interface IHomeWorkRepository
{
        Task<HomeWork> SubmitHomeworkAsync(HomeWork submission);
    Task<HomeWork?> GetSubmissionAsync(int submissionId);
    Task<HomeWork?> GetSubmissionForStudentAsync(int submissionId, int studentId);
    Task<HomeWork> UpdateSubmissionAsync(HomeWork submission);
    Task<bool> HasUngradedSubmissionAsync(int studentId, int homeTaskId);
    Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId);
}