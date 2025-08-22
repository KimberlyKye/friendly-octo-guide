using Common.Domain.ValueObjects;

namespace Application.Models.Teacher.Responses
{
    public class StudentAllCoursesModel
    {
        public int Id { get; set; }
        public CourseName Name { get; set; }
        public bool IsActive { get; set; }
    }
}
