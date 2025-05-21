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
    
        [HttpGet("period-calendar-data")]
        public async Task<IActionResult> GetPeriodCalendarData(GetTeacherCalendarDataRequestDto request)
        {
            if (request is null
                || request.StartDate <= new DateOnly(2000, 1, 1)
                || request.EndDate <= new DateOnly(2000, 1, 1))
            {
                return BadRequest();
            }
            var requestModel = new GetTeacherCalendarDataRequestModel() { TeacherId = request.TeacherId, StartDate = request.StartDate, EndDate = request.EndDate};
            var result = await _teacherCalendarService.GetPeriodCalendarData(requestModel);
            return Ok(result);
        }        
    }
}
