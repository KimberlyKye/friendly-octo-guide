using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Entities;

namespace Application.Services
{
    public class HomeWorkService : IHomeWorkService
    {
        public Task<HomeWork> GetSubmissionAsync(int submissionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsHomeworkGradedAsync(int studentId, int homeTaskId)
        {
            throw new NotImplementedException();
        }

        public Task<HomeWork> SubmitHomeworkAsync(HomeWork submission)
        {
            throw new NotImplementedException();
        }

        public Task<HomeWork> UpdateSubmissionAsync(HomeWork submission)
        {
            throw new NotImplementedException();
        }
    }
}