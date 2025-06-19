using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Teacher.Requests;
using Application.Services.Abstractions;
using Application.Models.Teacher.Requests;
using Application.Models.Teacher.Responses;
using Humanizer;
using Domain.ValueObjects;
using File = Domain.ValueObjects.File;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия преподавателя с занятием
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherLessonController : ControllerBase
    {
        private readonly ITeacherLessonService _teacherService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="teacherService"></param>
        public TeacherLessonController(ITeacherLessonService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Метод создания занятия
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель созданного в БД пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /create-lesson
        ///     {
        ///        "teacherId": 111,
        ///        "courseId": 222,
        ///        "lessonName": "Название занятия",
        ///        "lessonDescription": "Описание занятия",
        ///        "lessonStartDate": "2025-05-12T20:00:01.783Z",
        ///        "material": {
        ///              "path": "C/Prog",
        ///              "extension": "txt",
        ///              "name": "FileName"
        ///              }
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Возвращает id созданного занятия</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>        
        [HttpPost("create-lesson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLesson(CreateLessonRequestDto request)
        {
            if (request.LessonStartDate <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(request.LessonStartDate),
                    "Дата занятия должна быть в будущем");
                return BadRequest(ModelState);
            }

            LessonName lessonName = new LessonName(request.LessonName);

            var requestModel = new CreateLessonModel
            {
                TeacherId = (int)request.TeacherId,
                CourseId = (int)request.CourseId,
                LessonName = lessonName,
                LessonDescription = request.LessonDescription,
                LessonStartDate = request.LessonStartDate,
                Material = request.Material,
            };

            var result = await _teacherService.CreateLesson(requestModel);
            return Ok(result);
        }
    }
}
