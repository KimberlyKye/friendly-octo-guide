using Common.Domain.Entities;
using Common.Domain.ValueObjects;

namespace WebApi.Dto.Lesson.Responses
{
    public class LessonInfoByCourseResponse
    {
        public int CourseId { get; set; }
        public LessonName LessonName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Common.Domain.ValueObjects.File? Material { get; set; }
        public HomeTask? HomeTask { get; set; }
    }
}
