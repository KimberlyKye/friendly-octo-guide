using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;


namespace Application.Models.Course
{
    public class CourseInfoForStudentModel
    {
        public CourseState State { get; set; }
        public Common.Domain.Entities.Teacher Teacher { get; set; }
        public CourseName Name { get; set; }
        public string Description { get; set; }
        public Duration Duration { get; set; }
        public Score PassingScore { get; set; }
        public Score AverageScore { get; set; }
    }
}
