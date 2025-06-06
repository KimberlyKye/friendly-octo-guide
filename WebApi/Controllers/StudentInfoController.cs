using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Teacher.Requests;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия преподавателя с занятием
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentInfoController : Controller
    {
        private readonly IStudentInfoService _studentInfoService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="studentInfoService"></param>
        public StudentInfoController(IStudentInfoService studentInfoService)
        {
            _studentInfoService = studentInfoService;
        }

        /// <summary>
        /// Метод получения всех курсов Студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>Модель всех курсов, на которые записан Студент</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /get-all-courses
        ///     {
        ///        "studentId": 111
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Возвращает id созданного занятия</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>        
        [HttpGet("get-all-courses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCourses(int studentId)
        {
            if (studentId <= 0) { return BadRequest("studentId не может быть меньше или равен 0 "); }

            var  result = _studentInfoService.GetAllCourses(studentId);

            return Ok(result);
        }
    }
}
