using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesAbstractions.Abstractions
{
    public interface IStudentInfoRepository
    {
        Task<Student?> GetStudentById(int studentId);
        Task<List<Course>> GetAllCourses(int studentId);
    }
}
