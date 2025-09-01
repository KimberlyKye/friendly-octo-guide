using Application.Services.Abstractions;
using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для формирования календаря студента
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentCalendarController : ControllerBase
    {
        private readonly IStudentCalendarService _studentCalendarService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="studentCalendarService"></param>
        public StudentCalendarController(IStudentCalendarService studentCalendarService)
        {
            _studentCalendarService = studentCalendarService;
        }

        /// <summary>
        /// Получение данных календаря студента за указанный период
        /// </summary>
        /// <param name="request">Параметры запроса</param>
        /// <returns>Данные календаря студента</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/StudentCalendar/period-calendar-data?studentId=12345&startDate="2023-09-01"&endDate"2023-09-30"
        ///
        /// </remarks>
        /// <response code="200">Возвращает данные календаря студента</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Студент не найден</response>
        /// <response code="500">Ошибка сервера при обработке запроса</response>
        [HttpGet("period-calendar-data")]
        [ProducesResponseType(typeof(StudentCalendarResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeriodCalendarData(int studentId, DateTime startDate, DateTime endDate)
        {
            if (studentId <= 0)
            {
                return BadRequest();
            }
            var requestModel = new GetStudentCalendarDataRequestModel()
            {
                StudentId = studentId,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await _studentCalendarService.GetPeriodCalendarData(requestModel);
            return Ok(result);
        }
    }
}
