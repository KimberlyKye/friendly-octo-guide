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
        /// <param name="teacherCalendarService"></param>
        public StudentCalendarController(ITeacherCalendarService teacherCalendarService)
        {
            _teacherCalendarService = teacherCalendarService;
        }

        /// <summary>
        /// Получает данные календаря преподавателя за указанный период
        /// </summary>
        /// <param name="request">DTO с параметрами запроса</param>
        /// <returns>Данные календаря преподавателя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/StudentCalendar/period-calendar-data
        ///     ?TeacherId=12345
        ///     &amp;StartDate=2023-12-01
        ///     &amp;EndDate=2023-12-31
        ///
        /// </remarks>
        /// <response code="200">Возвращает данные календаря преподавателя за период</response>
        /// <response code="400">Если параметры запроса невалидны (неверный формат дат, отсутствуют обязательные поля)</response>
        /// <response code="500">При внутренней ошибке сервера</response>
        [HttpGet("period-calendar-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeriodCalendarData(GetTeacherCalendarDataRequestDto request)
        {
            var requestModel = new GetTeacherCalendarDataRequestModel() { TeacherId = request.TeacherId, StartDate = request.StartDate, EndDate = request.EndDate};
            var result = await _teacherCalendarService.GetPeriodCalendarData(requestModel);
            return Ok(result);
        }        
    }
}
