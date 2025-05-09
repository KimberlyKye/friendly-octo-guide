using Application.Services.Abstractions;
using Entities;
using Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeacherInfoService : ITeacherInfoService
    {        
        private readonly ITeacherInfoRepository _teacherInfoRepository;

        public TeacherInfoService(ITeacherInfoRepository teacherInfoRepository)
        {            
            _teacherInfoRepository = teacherInfoRepository;
        }
        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            return await _teacherInfoRepository.GetTeacherById(teacherId);
        }
    }
}
