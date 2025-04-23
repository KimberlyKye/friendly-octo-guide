using Entities;
using Domain.ValueObjects;

namespace Dto.Teacher.Requests
{
    public class CreateLessonRequestDto
    {
        public LessonName? LessonName {  get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public DateTime Date {  get; set; }
        public Domain.ValueObjects.File? Material { get; set; } = null;
        public List<HomeTask> HomeTasks { get; set; } = new List<HomeTask>();
    }
}
