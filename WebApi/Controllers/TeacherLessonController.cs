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
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherLessonController : ControllerBase
    {
        private readonly ITeacherLessonService _teacherService;

        public TeacherLessonController(ITeacherLessonService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet("calendar-data")]
        //public async Task<IActionResult> GetCalendarData(GetCalendarDataRequestDto requestDto)
        //{
        //    if (requestDto is null
        //        || requestDto.userId <= 0
        //        || requestDto.date <= new DateOnly(2000, 1, 1))
        //    {
        //        return BadRequest();
        //    }
        //    var result = await _teacherService.GetCalendarData(requestDto);
        //    return Ok(result);
        //}
        [HttpPost("create-lesson")]
        public async Task<IActionResult> CreateLesson(CreateLessonRequestDto requestDto)
        {           
            try
            {   LessonName lessonName = new LessonName(requestDto.LessonName);

                var requestModel = new CreateLessonModel
                {
                    TeacherId = (int)requestDto.TeacherId,
                    CourseId = (int)requestDto.CourseId,
                    LessonName = lessonName,
                    LessonDescription = requestDto.LessonDescription,
                    LessonStartDate = requestDto.LessonStartDate,
                    Material = requestDto.Material,
                };

                var result = await _teacherService.CreateLesson(requestModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (можно добавить _logger)
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
