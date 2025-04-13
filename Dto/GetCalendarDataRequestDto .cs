using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class GetCalendarDataRequestDto
    {
        [Required]
        public int userId {  get; set; }
        [Required]
        public DateOnly date {  get; set; }
    }
}
