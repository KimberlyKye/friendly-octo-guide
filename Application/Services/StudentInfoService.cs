using Application.Services.Abstractions;
using Application.Models.Teacher.Responses;
using RepositoriesAbstractions.Abstractions;
using Domain.ValueObjects.Enums;
using Entities;
using Application.Models.Course;
using Domain.ValueObjects;

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
        public async Task<List<StudentAllCoursesModel>> GetAllCourses(int studentId)
        {
            var courses = await _studentInfoRepository.GetAllCourses(studentId);
            
            var result = new List<StudentAllCoursesModel>();
            foreach (var course in courses)
            {
                result.Add(new StudentAllCoursesModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    IsActive = course.State == CourseState.Active
                });
            }
            return result;
        }
        public async Task<CourseInfoForStudentModel?> GetCourseInfo(int courseId, int studentId)
        {

            var course = await _studentInfoRepository.GetCourseInfo(courseId, studentId);
            if (course is null) { return null; }

            return new CourseInfoForStudentModel
            {
                State = course.State,
                Teacher = course.Teacher,
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration
            };
        }
        public async Task<List<Lesson?>> GetLessonsInfoByCourse(int courseId, int studentId)
        {
            return new List<Lesson?>();
        }
    }
}
