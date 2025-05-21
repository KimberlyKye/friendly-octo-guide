using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories
{
    public class TeacherCalendarRepository : ITeacherCalendarRepository
    {
        private readonly AppDbContext _context;
        private readonly ITeacherFactory _teacherFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;

        public TeacherCalendarRepository(
            AppDbContext context,
            ITeacherFactory teacherFactory,
            ICourseFactory courseFactory,
            ILessonFactory lessonFactory)
        {
            _context = context;
            _teacherFactory = teacherFactory;
            _courseFactory = courseFactory;
            _lessonFactory = lessonFactory;
        }
        public async Task<IReadOnlyCollection<Entities.Course>> GetPeriodCalendarData(int teacherId, DateOnly startDate, DateOnly endDate)
        {
            // Валидация входных параметров
            if (startDate > endDate)
            {
                //_logger.LogError("Дата начала не может быть позже даты окончания.");
                return Array.Empty<Entities.Course>().AsReadOnly();
            }

            try
            {
                // Преобразование DateOnly в DateTime для фильтрации в БД
                var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
                var endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

                // Оптимизированный запрос с JOIN
                var query =
                    from teacher in _context.Users.AsNoTracking()
                    join course in _context.Courses on teacher.Id equals course.TeacherId
                    join lesson in _context.Lessons on course.Id equals lesson.CourseId
                    where teacher.Id == teacherId
                        && teacher.RoleId == (int)RoleEnum.Teacher
                        && lesson.Date >= startDateTime
                        && lesson.Date <= endDateTime
                    select new { Teacher = teacher, Course = course, Lesson = lesson };

                var data = await query.ToListAsync();

                // Группировка данных по курсам
                var groupedData = data
                    .GroupBy(x => x.Course.Id)
                    .Select(g => new
                    {
                        Course = g.First().Course,
                        Teacher = g.First().Teacher,
                        Lessons = g.Select(x => x.Lesson).ToList(),
                        LessonIds = g.Select(x => x.Lesson.Id).Distinct().ToList()
                    })
                    .ToList();               

                var result = new List<Entities.Course>();

                foreach (var group in groupedData)
                {
                    try
                    {
                        // Создание доменного объекта курса
                        var domainCourse = await _courseFactory.CreateFrom(group.Course, group.Teacher);

                        // Маппинг уроков с HomeTasks
                        var domainLessons = new List<Entities.Lesson>();
                        
                        domainCourse.AddLessons(domainLessons);
                        result.Add(domainCourse);
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError(ex, "Ошибка маппинга курса {CourseId}", group.Course.Id);
                        return Array.Empty<Entities.Course>().AsReadOnly();
                    }
                }

                return result.AsReadOnly();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Ошибка получения данных для преподавателя {TeacherId}", teacherId);
                return Array.Empty<Entities.Course>().AsReadOnly();
            }
        }
    }
}
