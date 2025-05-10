using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstractions
{
    public interface ITeacherInfoService
    {
        public Task<Teacher> GetTeacherById(int teacherId);
    }
}
