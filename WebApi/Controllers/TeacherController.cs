using Microsoft.AspNetCore.Mvc;
using Application.Services.Abstractions;
using Dto.Teacher.Requests;
using Application.Dto.Teacher;
using Humanizer;
using Domain.ValueObjects;

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
            // Проверка на null
            if (requestDto is null)
            {
                return BadRequest("Request data cannot be null");
            }

            try
            {   LessonName lessonName = new LessonName(requestDto.LessonName);

                var requestModel = new CreateLessonModel
                {
                    TeacherId = requestDto.TeacherId,
                    CourseId = requestDto.CourseId,
                    LessonName = lessonName,
                    LessonDescription = requestDto.LessonDescription,
                    LessonStartDate = requestDto.LessonStartDate,
                    Material = requestDto.Material,
                    HomeTasks = requestDto.HomeTasks
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
