using Domain.ValueObjects.Enums;
using Domain.ValueObjects;

namespace WebApi.Dto.Course.Responses
{
    public class CourseInfoForStudentResponse
    {
        public CourseState State { get; set; }
        public Entities.Teacher Teacher { get; set; }
        public CourseName Name { get; set; }
        public string Description { get; set; }
        public Duration Duration { get; set; }
        public Score PassingScore { get; set; }
        public Score AverageScore { get; set; }
    }
}
