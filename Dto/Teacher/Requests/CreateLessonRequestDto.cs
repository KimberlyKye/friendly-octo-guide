using Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using File = Domain.ValueObjects.File;

namespace Dto.Teacher.Requests
{
    public class CreateLessonRequestDto
    {
        [Required]
        public long TeacherId { get; set; }
        [Required]
        public long CourseId { get; set; }
        [Required]
        public required string LessonName {  get; set; }
        [Required]
        public required string LessonDescription { get; set; }
        [Required]
        public DateTime LessonStartDate {  get; set; }
        [Required]
        public File? Material { get; set; }

        //public List<HomeTask>? HomeTasks { get; set; }
    }
}
