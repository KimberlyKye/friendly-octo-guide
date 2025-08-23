using Application.Models.Course;
using Application.Services.Abstractions;
using Domain.ValueObjects;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Requests;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер курса
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="courseService"></param>
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Метод создания курса
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        try
        {
            var createdCourseId = await _courseService.AddCourseAsync(new CreateCourseModel()
            {
                StateId = request.StateId,
                TeacherId = request.TeacherId,
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PassingScore = request.PassingScore
            });
            return StatusCode(201, createdCourseId);
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить курс по ID
    /// </summary>
    /// <param name="courseId">ID курса</param>
    /// <returns>Модель курса</returns>
    [HttpGet("{courseId:int}")]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCourseById(int courseId)
    {
        try
        {
            var course = await _courseService.GetCourseAsync(courseId);
            if (course == null)
                return NotFound();

            return Ok(new CourseResponse
            {
                Id = course.Id,
                StateId = (int)course.State,
                TeacherId = course.Teacher.Id,
                Name = course.Name.Value,
                Description = course.Description,
                StartDate = course.Duration.StartDate,
                EndDate = course.Duration.EndDate,
            });
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить все курсы студента
    /// </summary>
    /// <param name="userId">ID студента</param>
    /// <returns>Массив курсов</returns>
    [HttpGet("student/{userId:int}")]
    [ProducesResponseType(typeof(CourseResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllStudentCourses(int userId)
    {
        try
        {
            var courses = await _courseService.GetAllStudentCoursesAsync(userId);
            return Ok(courses.Select(course => new CourseResponse
            {
                Id = course.Id,
                StateId = (int)course.State,
                TeacherId = course.Teacher.Id,
                Name = course.Name.Value,
                Description = course.Description,
                StartDate = course.Duration.StartDate,
                EndDate = course.Duration.EndDate
            }).ToArray());
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить все курсы преподавателя
    /// </summary>
    /// <param name="userId">ID преподавателя</param>
    /// <returns>Массив курсов</returns>
    [HttpGet("teacher/{userId:int}")]
    [ProducesResponseType(typeof(CourseResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTeacherCourses(int userId)
    {
        try
        {
            var courses = await _courseService.GetAllTeacherCoursesAsync(userId);
            return Ok(courses.Select(course => new CourseResponse
            {
                Id = course.Id,
                StateId = (int)course.State,
                TeacherId = course.Teacher.Id,
                Name = course.Name.Value,
                Description = course.Description,
                StartDate = course.Duration.StartDate,
                EndDate = course.Duration.EndDate
            }).ToArray());
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить курс
    /// </summary>
    /// <param name="request">Данные для обновления</param>
    /// <returns>ID обновленного курса</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseRequest request)
    {
        try
        {
            var duration = new Duration(request.StartDate, request.EndDate);
            var updatedId = await _courseService.UpdateCourseAsync(new Course
            (
                 request.Id, request.TeacherId, new CourseName(request.Title), request.Description, duration
            ));

            if (updatedId <= 0)
                return BadRequest("Курс не найден или не удалось обновить");

            return Ok(updatedId);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is KeyNotFoundException)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить урок по ID
    /// </summary>
    /// <param name="id">ID урока</param>
    /// <returns>Модель урока</returns>
    [HttpGet("lesson/{id:int}")]
    [ProducesResponseType(typeof(LessonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLessonById(int id)
    {
        try
        {
            var lesson = await _courseService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();

            return Ok(new LessonResponse(lesson));
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить урок
    /// </summary>
    /// <param name="request">Данные для обновления</param>
    /// <returns>ID обновленного урока</returns>
    [HttpPut("lesson")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonRequest request)
    {
        try
        {
            var updatedId = await _courseService.UpdateLesson(new Lesson
            (
                request.Id,
                request.CourseId,
                new LessonName(request.Name),
                request.Description,
                request.Date,
                new Domain.ValueObjects.File(request.Material)
            ));

            if (updatedId <= 0)
                return BadRequest("Урок не найден или не удалось обновить");

            return Ok(updatedId);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is KeyNotFoundException)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}
