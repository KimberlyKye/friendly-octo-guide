using Application.Models.Course;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Requests;
using WebApi.Dto.Course.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер курса
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Метод создание курса
    /// </summary>
    /// <param name="courseData"></param>
    /// <returns></returns>
    /// <response code="200">Возвращает курс</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest courseData)
    {
        var createdCourse = await _courseService.AddCourseAsync(new CreateCourseModel()
        {
            StateId = courseData.StateId,
            TeacherId = courseData.TeacherId,
            Title = courseData.Title,
            Description = courseData.Description,
            StartDate = courseData.StartDate,
            EndDate = courseData.EndDate,
            PassingScore = courseData.PassingScore
        });

        var createdCourseResponse = new CreateCourseResponse()
        {
            Id = createdCourse.Id,
            StateId = (int)createdCourse.StateId,
            TeacherId = createdCourse.TeacherId,
            Title = createdCourse.Title,
            Description = createdCourse.Description,
            StartDate = createdCourse.StartDate,
            EndDate = createdCourse.EndDate,
            PassingScore = createdCourse.PassingScore
        };
        return Ok(createdCourseResponse);
    }

    private ICourseService _courseService;
}