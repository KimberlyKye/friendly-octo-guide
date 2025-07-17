using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Application.Services.Abstractions
{
    public interface IHomeWorkService
    {
        Task<HomeWork> GetSubmissionAsync(int submissionId);
        Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId);
        Task<HomeWork> SubmitHomeworkAsync(HomeWork submission);
        Task<HomeWork> UpdateSubmissionAsync(HomeWork submission);
    }
}