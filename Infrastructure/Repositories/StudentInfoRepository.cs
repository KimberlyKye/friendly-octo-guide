using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentInfoRepository : IStudentInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;

        public StudentInfoRepository(
            AppDbContext context,
            IStudentFactory studentFactory)
        {
            _context = context;
            _studentFactory = studentFactory;

        }
        public async Task<Student?> GetStudentById(int studentId)
        {

            var studentInfo = await _context.Users
                .Where(student => student.Id == studentId && student.RoleId == (int)RoleEnum.Student)
                .FirstOrDefaultAsync();

            if (studentInfo is null) { return null; }

            return await _studentFactory.CreateFromAsync(studentInfo);
        }
    }
}
