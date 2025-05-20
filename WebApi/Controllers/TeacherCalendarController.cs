using Application.Models.Calendar.Requests;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Calendar.Requests;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для формирования календаря преподавателя
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentCalendarController : ControllerBase
    {
        private readonly ITeacherCalendarService _teacherCalendarService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="teacherService"></param>
        public StudentCalendarController(ITeacherCalendarService teacherCalendarService)
        {
            _teacherCalendarService = teacherCalendarService;
        }
    
        [HttpGet("week-calendar-data")]
        public async Task<IActionResult> GetWeekCalendarData(GetTeacherCalendarDataRequestDto request)
        {
            if (request is null
                || request.Date <= new DateOnly(2000, 1, 1))
            {
                return BadRequest();
            }
            var requestModel = new GetTeacherCalendarDataRequestModel() { TeacherId = request.TeacherId, Date = request.Date};
            var result = await _teacherCalendarService.GetWeekCalendarData(requestModel);
            return Ok(result);
        }
    }
}
