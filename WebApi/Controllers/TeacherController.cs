using Microsoft.AspNetCore.Mvc;
using Dto;
using Application.Services.Abstractions;

namespace WebApi.Controllers
{
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCalendarData(GetCalendarDataRequestDto requestDto)
        {
            if (requestDto is null
                || requestDto.userId <= 0
                || requestDto.date <= new DateOnly(2000, 1, 1))
            {
                return BadRequest();
            }
            var result = await _teacherService.GetCalendarData(requestDto);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonRequestDto requestDto)
        {
            var result = await _teacherService.CreateLesson(requestDto);
            return  Ok(1);
        }
    }
}
