using Common.Domain.ValueObjects;

namespace WebApi.Dto.Student.Responses
{
    public class AllCoursesResponse
    {
        public int Id { get; set; }
        public CourseName Name { get; set; }
        public bool IsActive { get; set; }
    }
}
