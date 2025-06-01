using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;
using Application.Services;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Calendar.Requests;

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
        /// Получение данных календаря преподавателя за указанный период
        /// </summary>
        /// <param name="request">Параметры запроса</param>
        /// <returns>Данные календаря преподавателя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/StudentCalendar/period-calendar-data
        ///     {
        ///        "teacherId": 12345,
        ///        "startDate": "2023-09-01",
        ///        "endDate": "2023-09-30"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает данные календаря преподавателя</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Преподаватель не найден</response>
        /// <response code="500">Ошибка сервера при обработке запроса</response>
        [HttpGet("period-calendar-data")]
        [ProducesResponseType(typeof(StudentCalendarResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPeriodCalendarData(GetStudentCalendarDataRequestDto request)
        {
            var requestModel = new GetStudentCalendarDataRequestModel()
            {
                StudentId = request.StudentId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            var result = await _studentCalendarService.GetPeriodCalendarData(requestModel);
            return Ok(result);
        }
    }
}
