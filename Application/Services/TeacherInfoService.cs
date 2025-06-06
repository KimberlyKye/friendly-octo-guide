﻿using Application.Services.Abstractions;
using Entities;
using RepositoriesAbstractions.Abstractions;
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
            Teacher? teacher = await _teacherInfoRepository.GetTeacherById(teacherId);
            if (teacher is null)
            {
                throw new ArgumentException($"Преподаватель с ID {teacherId} не существует", nameof(teacherId));
            }
            return teacher;
        }
    }
}
