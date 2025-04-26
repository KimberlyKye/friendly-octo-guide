using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;

namespace Application.Services.Abstractions
{
    public interface IUpdateStudentProfileService
    {
        Task Execute(UpdateStudentProfileRequest request);
    }
}