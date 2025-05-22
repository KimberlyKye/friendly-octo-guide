using Application.Models.Calendar.Requests;
using Application.Services;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Calendar.Requests;

namespace WebApi.Controllers
{
    ///// <summary>
    ///// Контроллер для формирования календаря студента
    ///// </summary>
    //[ApiController]
    //[Route("api/[controller]")]
    //[Produces("application/json")]
    //public class StudentCalendarController : ControllerBase
    //{
    //    private readonly IStudentCalendarService _studentCalendarService;

    //    /// <summary>
    //    /// Конструктор
    //    /// </summary>
    //    /// <param name="studentService"></param>
    //    public StudentCalendarController(IStudentCalendarService studentCalendarService)
    //    {
    //        _studentCalendarService = studentCalendarService;
    //    }
    
    //    [HttpGet("calendar-data")]
    //    public async Task<IActionResult> GetCalendarData(GetStudentCalendarDataRequestDto requestDto)
    //    {
    //        if (requestDto is null
    //            || requestDto.StudentId <= 0
    //            || requestDto.Date <= new DateOnly(2000, 1, 1))
    //        {
    //            return BadRequest();
    //        }
    //        var requestModel = new GetStudentCalendarDataRequestModel() { StudentId = requestDto.StudentId, Date = requestDto.Date};
    //        var result = await _studentCalendarService.GetCalendarData(requestModel);
    //        return Ok(result);
    //    }
    //}
}
