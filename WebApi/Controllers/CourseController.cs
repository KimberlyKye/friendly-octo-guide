using Application.Models.Course;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course.Requests;
using WebApi.Dto.Course.Responses;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest courseData)
    {
        try
        {
            var createdCourse = await _courseService.AddCourseAsync(new CreateCourseModel()
            {
                Id = courseData.Id,
                StateId = (int)courseData.StateId,
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
        catch (Exception ex)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    private ICourseService _courseService;
}