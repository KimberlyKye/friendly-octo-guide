using Application.Services.Abstractions;
using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с календарем преподавателя
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherCalendarController : ControllerBase
    {
        private readonly ITeacherCalendarService _teacherCalendarService;

        /// <summary>
        /// Конструктор контроллера календаря преподавателя
        /// </summary>
        /// <param name="teacherCalendarService">Сервис для работы с календарем преподавателя</param>
        public TeacherCalendarController(ITeacherCalendarService teacherCalendarService)
        {
            _teacherCalendarService = teacherCalendarService;
        }

        /// <summary>
        /// Получение данных календаря преподавателя за указанный период
        /// </summary>
        /// <param name="request">Параметры запроса</param>
        /// <returns>Данные календаря преподавателя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/TeacherCalendar/period-calendar-data?teacherId=12345&startDate="2023-09-01"&endDate"2023-09-30"
        ///
        /// </remarks>
        /// <response code="200">Возвращает данные календаря преподавателя</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Преподаватель не найден</response>
        /// <response code="500">Ошибка сервера при обработке запроса</response>
        [HttpGet("period-calendar-data")]
        [ProducesResponseType(typeof(TeacherCalendarResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeriodCalendarData(int teacherId, DateTime startDate, DateTime endDate)
        {
            if (teacherId <= 0)
            {
                return BadRequest();
            }
            var requestModel = new GetTeacherCalendarDataRequestModel()
            {
                TeacherId = teacherId,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await _teacherCalendarService.GetPeriodCalendarData(requestModel);
            return Ok(result);
        }
    }
}