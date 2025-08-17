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
        Task<Course?> GetCourseInfo(int courseId, int studentId);
        Task<Course?> GetAllCourseInfo(int courseId, int studentId);
        Task<Lesson?> GetHomeworksInfo(int lessonId, int studentId); 
    }
}
