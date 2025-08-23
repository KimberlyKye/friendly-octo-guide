using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Responses;
using WebApi.Dto.Lesson.Responses;
using WebApi.Dto.Student.Requests;
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
        /// <response code="500">Если есть какие-то ошибки при создании</response>        
        [HttpGet("get-all-courses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCourses(int studentId)
        {
            if (studentId <= 0) { return BadRequest("studentId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetAllCourses(studentId);

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
        ///        "courseId": 111
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Курс не найден</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>        
        [HttpGet("get-course-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourseInfo(int courseId, int studentId)
        {
            if (courseId <= 0) { return BadRequest("courseId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetCourseInfo(courseId, studentId);

            if (result is null) { return NotFound(); }
            ;

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
        /// Метод получения информации о курсе (без уроков и ДЗ)
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns>Модель курса</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /get-lessons-info-by-course
        ///     {
        ///        "courseId": 111
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает модель</response>
        /// <response code="400">Некорректные параметры запроса</response>
        /// <response code="404">Курс не найден</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>        
        [HttpGet("get-lessons-info-by-course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLessonsInfoByCourse(int courseId, int studentId)
        {
            if (courseId <= 0) { return BadRequest("courseId не может быть меньше или равен 0 "); }

            var result = await _studentInfoService.GetLessonsInfoByCourse(courseId, studentId);
            if (result is null) { return NotFound(); }
            ;

            var response = result.Select(lesson => new LessonInfoByCourseResponse
            {
                CourseId = lesson.CourseId,
                LessonName = lesson.LessonName,
                Description = lesson.Description,
                Date = lesson.Date,
                Material = lesson.Material,
                HomeTasks = lesson.HomeTasks
            }).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Метод получения списка студентов, участвующих в курсе
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>Список студентов курса</returns>
        /// <remarks>
        /// Пример запроса:
        ///     GET /list/include-in-course/101
        ///</remarks>
        [HttpGet("list/include-in-course/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentListByCourse(int courseId)
        {
            if (courseId <= 0)
            {
                return BadRequest("courseId не может быть меньше или равен 0 ");
            }
            var result = await _studentInfoService.GetAllStudentsByCourse(courseId);
            if (result is null)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Метод получения списка студентов, не участвующих в курсе
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <returns>Список студентов, готовых для добавления на курс</returns>
        /// <remarks>
        /// Пример запроса:
        ///     GET /list/not-on-course/101
        ///</remarks>
        [HttpGet("list/not-on-course/{courseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudentListOutsideFromCourse([FromRoute] int courseId, [FromQuery] int startRow, [FromQuery] int endRow)
        {
            if (courseId <= 0)
            {
                return BadRequest("courseId не может быть меньше или равен 0 ");
            }
            var result = await _studentInfoService.GetAllStudentsOutsideCourse(courseId, startRow, endRow);
            if (result is null)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Метод добавления студентов в курс
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentIds"></param>
        /// <returns>Возвращает сообщение об успешности/неуспешности операции</returns>
        /// <remarks>
        /// Пример запроса:
        /// POST /add-to-course
        /// {
        ///    "courseId": 111,
        ///    "studentIds": [1,2,3]
        /// }
        /// </remarks>
        [HttpPost("add-to-course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddStudentToCourse([FromBody] StudentsToCourseRequest data)
        {
            if (data is null)
            {
                return BadRequest("Некорректные параметры запроса");
            }
            if (data.studentIds is null)
            {
                return BadRequest("Нет студентов для добавления");
            }
            if (data.courseId <= 0)
            {
                return BadRequest("courseId не может быть меньше или равен 0 ");
            }
            var result = await _studentInfoService.AddStudentsToCourse(data.courseId, data.studentIds);
            if (result.Count() == data.studentIds.Count())
            {
                return Ok("Студенты успешно добавлены");
            }

            var unsuccess = data.studentIds.Except(result);

            if (unsuccess.Count() == data.studentIds.Count())
            {
                return BadRequest("Не удалось добавить всех студентов!");
            }

            return Ok("Студенты частично успешно добавлены, однако в процессе возникли проблемы. При необходимости обновите страницу и повторите операцию. Не удалось добавить следующее количество студентов: " + unsuccess.Count() + " с ID: " + string.Join(", ", unsuccess));
        }

        /// <summary>
        /// Метод удаления студентов из курса
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentIds"></param>
        /// <returns> Возвращает сообщение об успешности/неуспешности операции</returns>
        /// <remarks>
        /// Пример запроса:
        /// POST /remove-from-course
        /// {
        ///    "courseId": 111,
        ///    "studentIds": [1,2,3]
        /// }
        /// </remarks>
        [HttpPost("remove-from-course")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveStudentsFromCourse([FromBody] StudentsToCourseRequest data)
        {
            if (data is null)
            {
                return BadRequest("Некорректные параметры запроса");
            }

            var courseId = data.courseId;
            var studentIds = data.studentIds;

            if (studentIds is null)
            {
                return BadRequest("Отсутствуют студенты для удаления");
            }


            if (courseId <= 0)
            {
                return BadRequest("courseId не может быть меньше или равен 0 ");
            }

            var result = await _studentInfoService.RemoveStudentsFromCourse(courseId, studentIds);
            if (result.Count() == studentIds.Count())
            {
                return Ok("Студенты успешно удалены");
            }
            var unsuccess = studentIds.Except(result);

            if (unsuccess.Count() == studentIds.Count())
            {
                return BadRequest("Не удалось удалить всех студентов!");
            }

            return Ok("Студенты частично успешно удалены, однако в процессе возникли проблемы. При необходимости обновите страницу и повторите операцию. Не удалось удалить следующее количество студентов: " + unsuccess.Count() + " с ID: " + string.Join(", ", unsuccess));
        }

    }
}
