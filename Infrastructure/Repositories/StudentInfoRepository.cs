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
        private readonly ICourseFactory _courseFactory;


        public StudentInfoRepository(
            AppDbContext context,
            IStudentFactory studentFactory,
            ICourseFactory courseFactory)
        {
            _context = context;
            _studentFactory = studentFactory;
            _courseFactory = courseFactory;
        }
        
        public async Task<Student?> GetStudentById(int studentId)
        {

            var studentInfo = await _context.Users
                .Where(student => student.Id == studentId && student.RoleId == (int)RoleEnum.Student)
                .FirstOrDefaultAsync();

            if (studentInfo is null) { return null; }

            return await _studentFactory.CreateFromAsync(studentInfo);
        }
        public async Task<List<Course>> GetAllCourses(int studentId)
        {
            var coursesData = await(
                from student in _context.Users.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.StudentId == student.Id)
                from course in _context.Courses
                    .Where(c => c.Id == studentCourse.CourseId)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where student.Id == studentId
                    && student.RoleId == (int)RoleEnum.Student
                select new { Course = course, Teacher = teacher })
                .ToListAsync();

            var result = new List<Course>();
            foreach (var item in coursesData)
            {                
                var domainCourse = await _courseFactory.CreateFrom(
                    courseModel: item.Course,
                    teacher: item.Teacher);

                result.Add(domainCourse);
                
            }
            return result;
        }
    }
}
