using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Entities;

namespace Application.Services.Abstractions
{
    public interface ICreateStudentProfileService
    {
        Task<Student> Execute(CreateStudentProfileRequest request);
    }
}