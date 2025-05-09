using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Teacher.Requests
{
    public class GetCalendarDataRequestDto
    {
        [Required]
        public int UserId {  get; set; }
        [Required]
        public DateOnly Date {  get; set; }
    }
}
