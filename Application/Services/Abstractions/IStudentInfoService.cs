using Application.Models.Teacher.Responses;


namespace Application.Services.Abstractions
{
    public interface IStudentInfoService
    {
        Task <List<StudentAllCoursesModel>> GetAllCourses(int studentId);
    }
}
