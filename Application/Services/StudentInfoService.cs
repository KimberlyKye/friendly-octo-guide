using Application.Services.Abstractions;
using Application.Models.Teacher.Responses;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services
{
    public class StudentInfoService : IStudentInfoService
    {
        private readonly IStudentInfoRepository _studentInfoRepository;

        public StudentInfoService(
            IStudentInfoRepository studentInfoRepository)
        {
            _studentInfoRepository = studentInfoRepository;
        }
        public Task<List<StudentAllCoursesModel>> GetAllCourses(int studentId)
        {
            var courses = _studentInfoRepository.GetAllCourses(studentId);
            
            return 
        }
    }
}
