using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Entities;

namespace Application.Models.Course
{
    public class CourseInfoForStudentModel
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
