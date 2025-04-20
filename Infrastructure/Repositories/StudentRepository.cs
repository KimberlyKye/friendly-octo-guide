using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;
using Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;
        private readonly IHomeTaskFactory _homeTaskFactory;


        public StudentRepository(
            AppDbContext context,
             IStudentFactory studentFactory,
             ICourseFactory courseFactory,
            ILessonFactory lessonFactory,
            IHomeTaskFactory homeTaskFactory
        )
        {
            _context = context;
            _studentFactory = studentFactory;
            _courseFactory = courseFactory;
            _lessonFactory = lessonFactory;
            _homeTaskFactory = homeTaskFactory;
        }

        public async Task<IEnumerable<int>> GetCourseIdsForStudentAsync(int studentId)
        {
            var courseIds = await _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.CourseId)
                .ToArrayAsync();

            return courseIds;
        }

        public async Task<List<Entities.Lesson>> GetLessonsByDateRangeAndStudentAsync(int studentId, DateTime startDate, DateTime endDate)
        {
            var courseIds = await GetCourseIdsForStudentAsync(studentId);

            var lessons = await _context.Lessons
                .Where(l => courseIds.Contains(l.CourseId) && l.Date >= startDate && l.Date <= endDate)
                .ToListAsync();

            var lessonsDomain = new List<Entities.Lesson>();
            // foreach (var lesson in lessons)
            // {
            //     var lessonDomain = _lessonFactory.CreateFrom(lesson);
            //     lessonsDomain.Add(lessonDomain);
            // };

            return lessonsDomain;
        }

        public async Task<List<HomeTask>> GetHomeTasksByDeadlineRangeAndStudentAsync(int studentId, DateTime startDate, DateTime endDate)
        {
            var courseIds = await GetCourseIdsForStudentAsync(studentId);
            var validLessonIds = await _context.Lessons
                .Where(l => courseIds.Contains(l.CourseId))
                .Select(l => l.Id)
                .ToArrayAsync();

            return await _context.HomeTasks
                .Where(ht => validLessonIds.Contains(ht.LessonId) && ht.EndDate >= startDate && ht.EndDate <= endDate)
                .ToListAsync();
        }

        public async Task<(List<Entities.Lesson> Lessons, List<HomeTask> HomeTasks)> GetMonthlyDataForStudentAsync(int studentId, DateTime monthStart, DateTime monthEnd)
        {
            var lessons = await GetLessonsByDateRangeAndStudentAsync(studentId, monthStart, monthEnd);
            var homeTasks = await GetHomeTasksByDeadlineRangeAndStudentAsync(studentId, monthStart, monthEnd);

            return (lessons, homeTasks);
        }
    }
}