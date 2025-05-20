using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Calendar.Requests
{
    public class GetStudentCalendarDataRequestDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
    }
}
