using Entities;
using Infrastructure.DataModels;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Dto.Teacher.Responses;
using Dto.Teacher.Requests;

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
                .AsNoTracking()
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
                CalendarLessonDtos = lessonsWithTasks
                    .Select(x => new CalendarLessonDto
                    {
                        CourseId = x.Course.Id,
                        CourseName = x.Course.Title,
                        Date = x.Lesson.Date,
                        Name = x.Lesson.Title
                    }).ToArray(),

                CalendarHomeTaskDtos = lessonsWithTasks
                    .Where(x => x.HomeTask != null)
                    .Select(x => new CalendarHomeTaskDto
                    {
                        CourseId = x.Course.Id,
                        CourseName = x.Course.Title,
                        LessonId = x.Lesson.Id,
                        LessonName = x.Lesson.Title,
                        DateStart = x.HomeTask.StartDate,
                        DateEnd = x.HomeTask.EndDate,
                        Name = x.HomeTask.Title
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

    public async Task<int> AddLesson(Entities.Lesson lesson) //<Entities.Lesson>
    {
        //Domain.lesson  --->  DataModels.lesson(фабрикаМаппинг)

        return 1;
    }
}
