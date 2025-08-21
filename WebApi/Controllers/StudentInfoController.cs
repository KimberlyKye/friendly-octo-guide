using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Responses;
using WebApi.Dto.Lesson.Responses;
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
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="500">Если есть какие-то ошибки при запросе</response>
        [HttpGet("get-all-courses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCourses(int studentId)
        {
            if (studentId <= 0) { return BadRequest("studentId не может быть меньше или равен 0 "); }

            var  result = await _studentInfoService.GetAllCourses(studentId);

            return Ok(result);
        }

        /// <summary>
        /// Метод получения информации о курсе (без уроков и ДЗ)
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>Модель курса</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /get-course-info
        ///     {
        ///        "courseId": 111,
        ///        "studentId": 111
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Данные не найдены</response>
        /// <response code="500">Если есть какие-то ошибки при запросе</response>
        [HttpGet("get-course-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseInfo(int courseId, int studentId)
        {
            if (courseId <= 0 || studentId <= 0) { return BadRequest("courseId или studentId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetCourseInfo(courseId, studentId);

            if (result is null) { return NotFound(); };

            return Ok(new CourseInfoForStudentResponse
            {
                State = result.State,
                Teacher = result.Teacher,
                Name = result.Name,
                Description = result.Description,
                Duration = result.Duration
            });
        }
        /// <summary>
        /// Метод получения информации о уроках в курсе
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>Модель данных</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /get-lessons-info-by-course
        ///     {
        ///        "courseId": 111,
        ///        "studentId": 111
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Данные не найдены</response>
        /// <response code="500">Если есть какие-то ошибки при запросе</response>
        [HttpGet("get-lessons-info-by-course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLessonsInfoByCourse(int courseId, int studentId)
        {
            if (courseId <= 0 || studentId <= 0) { return BadRequest("courseId или studentId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetLessonsInfoByCourse(courseId, studentId);
            if (result is null) { return NotFound(); };

            var response = result.Select(lesson => new LessonInfoByCourseResponse
            {
                CourseId = lesson.CourseId,
                LessonName = lesson.LessonName,
                Description = lesson.Description,
                Date = lesson.Date,
                Material = lesson.Material,
                HomeTasks = lesson.HomeTask
            }).ToList();

            return Ok(response);
        }
        /// <summary>
        /// Метод получения полной информации о уроке и сданных домашних работах
        /// </summary>
        /// <param name="lessonId"></param>
        /// <param name="studentId"></param>
        /// <returns>Модель данных</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /get-homeworks-info
        ///     {
        ///        "lessonId": 111,
        ///        "studentId": 111,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Данные не найдены</response>
        /// <response code="500">Если есть какие-то ошибки при запросе</response>
        [HttpGet("get-homeworks-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeworksInfo(int lessonId, int studentId)
        {
            if (lessonId <= 0 || studentId <= 0) { return BadRequest("lessonId или studentId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetHomeworksInfo(lessonId, studentId);
            if (result is null) { return NotFound(); };





            return NotFound();
        }

    }
}
