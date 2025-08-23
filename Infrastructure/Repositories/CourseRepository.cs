using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ICourseFactory _courseFactory;
    private readonly ILessonFactory _lessonFactory;
    private readonly ITeacherFactory _teacherFactory;
    private readonly IStudentFactory _studentFactory;
    private readonly AppDbContext _context;

    public CourseRepository(
        AppDbContext context,
        ICourseFactory courseFactory,
        ILessonFactory lessonFactory,
        ITeacherFactory teacherFactory,
        IStudentFactory studentFactory
    )
    {
        _context = context;
        _courseFactory = courseFactory;
        _lessonFactory = lessonFactory;
        _teacherFactory = teacherFactory;
        _studentFactory = studentFactory;
    }

    public async Task<int> AddCourseAsync(Entities.Course course)
    {
        var courseDataModel = _courseFactory.MapTo(course);

        await _context.Courses.AddAsync(courseDataModel);
        await _context.SaveChangesAsync();
        return courseDataModel.Id;
    }

    public async Task<Entities.Course[]> GetAllStudentCoursesAsync(Student student)
    {
        // 1. Получаем студента
        var s = await _studentFactory.CreateDataModelAsync(student);

        // 2. Получаем курсы, в которых состоит студент
        var courseIds = await _context.StudentCourses
            .Where(sc => sc.StudentId == student.Id)
            .Select(sc => sc.CourseId)
            .ToListAsync();

        // 3. Получаем полные данные курсов
        var courses = await _context.Courses
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync();

        // 4. Асинхронно преобразуем курсы с учётом студента
        var courseTasks = courses.Select(async c => await _courseFactory.CreateFrom(c, s));

        // 5. Ждём завершения всех задач и возвращаем результат
        return await Task.WhenAll(courseTasks);
    }


    public async Task<Entities.Course[]> GetAllTeacherCoursesAsync(Teacher teacher)
    {
        // 1. Получаем преподавателя
        var t = await _teacherFactory.CreateDataModelAsync(teacher);

        // 2. Получаем курсы преподавателя
        var courses = await _context.Courses
            .Where(c => c.TeacherId == teacher.Id)
            .ToListAsync();

        // 3. Асинхронно преобразуем курсы
        var courseTasks = courses.Select(async c => await _courseFactory.CreateFrom(c, t));

        // 4. Ждём завершения всех задач и возвращаем результат
        return await Task.WhenAll(courseTasks);
    }


    public async Task<Entities.Course> GetCourseAsync(int courseId)
    {
        var courseDataModel = await _context.Courses.FindAsync(courseId);
        if (courseDataModel != null)
        {
            var teacher = await _context.Users.FindAsync(courseDataModel.TeacherId);
            var courseEntity = await _courseFactory.CreateFrom(courseDataModel, teacher);
            return courseEntity;
        }
        return null;
    }

    public async Task<Entities.Lesson> GetLessonByIdAsync(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson != null)
        {
            var lessonEntity = await _lessonFactory.CreateAsync(lesson);
            return lessonEntity;
        }
        return null;
    }

    public async Task<int> UpdateCourseAsync(Entities.Course course)
    {
        var dataModel = _courseFactory.MapTo(course);
        _ = _context.Courses.Update(dataModel);
        _ = await _context.SaveChangesAsync();
        return dataModel.Id;
    }

    public async Task<int> UpdateLesson(Entities.Lesson lesson)
    {
        var dataModel = await _lessonFactory.CreateDataModelAsync(lesson);
        _ = _context.Lessons.Update(dataModel);
        _ = await _context.SaveChangesAsync();
        return dataModel.Id;

    }
}