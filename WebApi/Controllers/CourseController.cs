using Application.Models.Course;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Requests;
using WebApi.Dto.Course.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер курса
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CourseController : ControllerBase
{
    private ICourseService _courseService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="courseService"></param>
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Метод создание курса
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="200">Возвращает курс</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
}