using Entities;
using Infrastructure.DataModels;
using Infrastructure.Contexts;
using Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Infrastructure.Factories.Abstractions;
using Infrastructure.Factories;
using Domain.ValueObjects.Enums;
using ValueObjects.Enums;

namespace Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;
    private readonly ITeacherFactory _teacherFactory;
    private readonly ILessonFactory _lessonFactory;

    public TeacherRepository(
        AppDbContext context,
        ITeacherFactory teacherFactory,
        ILessonFactory lessonFactory)
    {
        _context = context;
        _teacherFactory = teacherFactory;
        _lessonFactory = lessonFactory;
    }
    public async Task<Teacher> GetTeacherById(int teacherId)
    {
        if (teacherId <= 0)
            throw new ArgumentException("Invalid teacher ID", nameof(teacherId));

        try
        {
            var teacherInfo = await _context.Users
                .Where(teacher => teacher.Id == teacherId && teacher.RoleId == (int)RoleEnum.Teacher)
                .FirstOrDefaultAsync();
            
            if (teacherInfo == null) { throw new Exception(); }

            return await _teacherFactory.CreateFrom(teacherInfo);
        }
        catch
        {
            //_logger.LogError(ex, "Error while getting teacher by ID {TeacherId}", teacherId);
            throw;
        }
    }
    public async Task<bool> CheckIsCourseExistAndActiveById(int courseId)
    {
        return await _context.Courses
            .AnyAsync(c => c.Id == courseId && c.StateId == (int)CourseState.Active);
    }
    public async Task<int> AddLesson(Entities.Lesson lesson) //<Entities.Lesson>
    {
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson));

        try
        {
            // Преобразуем доменную модель в DataModel
            var lessonDataModel = await _lessonFactory.CreateDataModelAsync(lesson);

            // Добавляем в контекст EF Core
            await _context.Lessons.AddAsync(lessonDataModel);

            // Сохраняем изменения
            await _context.SaveChangesAsync();

            // Возвращаем ID созданной записи
            return lessonDataModel.Id;
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error while adding lesson");
            throw; // Перебрасываем исключение для обработки на уровне выше
        }
    }
    //public async Task<CalendarResponseModel> GetCalendarData(GetCalendarDataRequestModel requestDto)
    //{
    //    try
    //    {
    //        var lessonsWithTasks = await (
    //            from course in _context.Courses
    //            .AsNoTracking()
    //            from lesson in _context.Lessons
    //                .Where(l => l.CourseId == course.Id
    //                         && DateOnly.FromDateTime(l.Date) >= requestDto.date
    //                         && DateOnly.FromDateTime(l.Date) < requestDto.date.AddMonths(1))
    //            from homeTask in _context.HomeTasks
    //                .Where(hT => hT.LessonId == lesson.Id)
    //                .DefaultIfEmpty()
    //            where course.TeacherId == requestDto.userId
    //            select new
    //            {
    //                Course = course,
    //                Lesson = lesson,
    //                HomeTask = homeTask
    //            })
    //            .ToListAsync();

    //        var result = new CalendarResponseDto
    //        {
    //            CalendarLessonDtos = lessonsWithTasks
    //                .Select(x => new CalendarLessonDto
    //                {
    //                    CourseId = x.Course.Id,
    //                    CourseName = x.Course.Title,
    //                    Date = x.Lesson.Date,
    //                    Name = x.Lesson.Title
    //                }).ToArray(),

    //            CalendarHomeTaskDtos = lessonsWithTasks
    //                .Where(x => x.HomeTask != null)
    //                .Select(x => new CalendarHomeTaskDto
    //                {
    //                    CourseId = x.Course.Id,
    //                    CourseName = x.Course.Title,
    //                    LessonId = x.Lesson.Id,
    //                    LessonName = x.Lesson.Title,
    //                    DateStart = x.HomeTask.StartDate,
    //                    DateEnd = x.HomeTask.EndDate,
    //                    Name = x.HomeTask.Title
    //                }).ToArray()
    //        };

    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        // Логирование ошибки (рекомендуется использовать ILogger)
    //        Console.WriteLine($"Error getting calendar data: {ex.Message}");
    //        return new CalendarResponseDto(); // Возвращаем пустой объект при ошибках ??
    //    }
    //}
}
