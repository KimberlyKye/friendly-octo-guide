﻿using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using RepositoriesAbstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Factories.Abstractions;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    public class TeacherInfoRepository : ITeacherInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly ITeacherFactory _teacherFactory;

        public TeacherInfoRepository(
            AppDbContext context,
            ITeacherFactory teacherFactory)
        {
            _context = context;
            _teacherFactory = teacherFactory;

        }
        public async Task<Teacher?> GetTeacherById(int teacherId)
        {
            
            var teacherInfo = await _context.Users
                .Where(teacher => teacher.Id == teacherId && teacher.RoleId == (int)RoleEnum.Teacher)
                .FirstOrDefaultAsync();

            if (teacherInfo is null) { return null; }

            return await _teacherFactory.CreateFrom(teacherInfo);            
        }
    }
}
