using Domain.ValueObjects;
using Entities;

namespace WebApi.Dto.Lesson.Responses
{
    public class LessonInfoByCourseResponse
    {
        public int CourseId { get; set; }
        public LessonName LessonName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Domain.ValueObjects.File? Material { get; set; }
        public HomeTask? HomeTask { get; set; }
    }
}
