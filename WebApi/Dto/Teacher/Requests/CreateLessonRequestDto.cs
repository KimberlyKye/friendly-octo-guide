using Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using File = Domain.ValueObjects.File;

namespace WebApi.Dto.Teacher.Requests
{
    public class CreateLessonRequestDto
    {
        [Required]
        public long TeacherId { get; set; }
        [Required]
        public long CourseId { get; set; }
        [Required]
        public string LessonName { get; set; }
        [Required]
        public string LessonDescription { get; set; }
        [Required]
        public DateTime LessonStartDate { get; set; }
        public File? Material { get; set; }
    }
}
