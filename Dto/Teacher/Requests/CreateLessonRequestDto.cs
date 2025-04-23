using Entities;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Dto.Teacher.Requests
{
    public class CreateLessonRequestDto
    {
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public required string LessonName {  get; set; }
        [Required]
        public string LessonDescription { get; set; } = string.Empty;
        [Required]
        public DateTime LessonStartDate {  get; set; }
        [Required]
        public Domain.ValueObjects.File? Material { get; set; }
        [Required]
        public required List<HomeTask> HomeTasks { get; set; }
    }
}
