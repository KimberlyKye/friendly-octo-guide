using Dto;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto)
    {
        try
        {
            var lessonsWithTasks = await (
                from course in _context.Courses
                from lesson in _context.Lessons
                    .Where(l => l.CourseId == course.Id
                             && DateOnly.FromDateTime(l.Date) >= requestDto.date
                             && DateOnly.FromDateTime(l.Date) < requestDto.date.AddMonths(1))
                from homeTask in _context.HomeTasks
                    .Where(hT => hT.LessonId == lesson.Id)
                    .DefaultIfEmpty()
                where course.TeacherId == requestDto.userId
                select new
                {
                    Course = course,
                    Lesson = lesson,
                    HomeTask = homeTask
                })
                .ToListAsync();

            var result = new CalendarResponseDto
            {
                calendarLessonDtos = lessonsWithTasks
                    .Select(x => new CalendarLessonDto
                    {
                        courseId = x.Course.Id,
                        courseName = x.Course.Title,
                        date = x.Lesson.Date,
                        name = x.Lesson.Title
                    }).ToArray(),

                calendarHomeTaskDtos = lessonsWithTasks
                    .Where(x => x.HomeTask != null)
                    .Select(x => new CalendarHomeTaskDto
                    {
                        courseId = x.Course.Id,
                        courseName = x.Course.Title,
                        lessonId = x.Lesson.Id,
                        lessonName = x.Lesson.Title,
                        dateStart = x.HomeTask.StartDate,
                        dateEnd = x.HomeTask.EndDate,
                        name = x.HomeTask.Title
                    }).ToArray()
            };

            return result;
        }
        catch (Exception ex)
        {
            // Логирование ошибки (рекомендуется использовать ILogger)
            Console.WriteLine($"Error getting calendar data: {ex.Message}");
            return new CalendarResponseDto(); // Возвращаем пустой объект при ошибках ??
        }

    }
}
