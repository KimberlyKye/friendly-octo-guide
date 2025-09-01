using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.RepositoriesAbstractions;

namespace Application.Services
{
    public class HomeWorkService : IHomeWorkService
    {
        private readonly IHomeWorkRepository _homeWorkRepository;

        public HomeWorkService(IHomeWorkRepository homeWorkRepository)
        {
            _homeWorkRepository = homeWorkRepository;
        }
        public Task<HomeWork> GetSubmissionAsync(int submissionId)
        {
            return _homeWorkRepository.GetSubmissionAsync(submissionId);
        }

        public Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId)
        {
            return _homeWorkRepository.IsHomeworkGradedAsync(studentId, homeTaskId);
        }

        public Task<HomeWork> SubmitHomeworkAsync(HomeWork submission)
        {
            return _homeWorkRepository.SubmitHomeworkAsync(submission);
        }

        public Task<HomeWork> UpdateSubmissionAsync(HomeWork submission)
        {
            return _homeWorkRepository.UpdateSubmissionAsync(submission);
        }
    }
}