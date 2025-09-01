using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Infrastructure.Contexts;
using Infrastructure.DataModels;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories
{
    public class StudentInfoRepository : IStudentInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;
        private readonly ICourseFactory _courseFactory;
        private readonly ILessonFactory _lessonFactory;
        private readonly IHomeTaskFactory _homeTaskFactory;
        private readonly IHomeWorkFactory _homeWorkFactory;


        public StudentInfoRepository(
            AppDbContext context,
            IStudentFactory studentFactory,
            ICourseFactory courseFactory,
            ILessonFactory lessonFactory,
            IHomeTaskFactory homeTaskFactory,
            IHomeWorkFactory homeWorkFactory)
        {
            _context = context;
            _studentFactory = studentFactory;
            _courseFactory = courseFactory;
            _lessonFactory = lessonFactory;
            _homeTaskFactory = homeTaskFactory;
            _homeWorkFactory = homeWorkFactory;
        }

        public async Task<Student?> GetStudentById(int studentId)
        {
            var studentInfo = await _context.Users
                .Where(student => student.Id == studentId && student.RoleId == (int)RoleEnum.Student)
                .FirstOrDefaultAsync();

            if (studentInfo is null) { return null; }

            return await _studentFactory.CreateFromAsync(studentInfo);
        }

        public async Task<List<Common.Domain.Entities.Course>> GetAllCourses(int studentId)
        {
            var coursesData = await (
                from student in _context.Users.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.StudentId == student.Id)
                from course in _context.Courses
                    .Where(c => c.Id == studentCourse.CourseId)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where student.Id == studentId
                    && student.RoleId == (int)RoleEnum.Student
                select new { Course = course, Teacher = teacher })
                .ToListAsync();

            var result = new List<Common.Domain.Entities.Course>();
            foreach (var item in coursesData)
            {
                var domainCourse = await _courseFactory.CreateFrom(
                    courseModel: item.Course,
                    teacher: item.Teacher);

                result.Add(domainCourse);

            }
            return result;
        }
        public async Task<Common.Domain.Entities.Course?> GetCourseInfo(int courseId, int studentId)
        {
            var courseData = await (
                from course in _context.Courses.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.CourseId == course.Id && sc.StudentId == studentId)
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where course.Id == courseId
                select new { course, teacher })
                .FirstOrDefaultAsync();

            if (courseData is null) { return null;};

            Score? averageScore;
            averageScore = await GetCourseAverageScore(courseId, studentId);

            return await _courseFactory.CreateFrom(
                courseModel: courseData.course,
                teacher: courseData.teacher,
                averageScore: averageScore);
        }
        public async Task<Common.Domain.Entities.Course?> GetAllCourseInfo(int courseId, int studentId)
        {
            var coursesData = await (
                from course in _context.Courses.AsNoTracking()
                from studentCourse in _context.StudentCourses
                    .Where(sc => sc.CourseId == course.Id && sc.StudentId == studentId)
                from lessons in _context.Lessons
                    .Where(l => l.CourseId == course.Id)
                from lessonScore in _context.LessonScores
                    .Where(lS => lS.LessonId == lessons.Id && lS.StudentId == studentId)
                    .DefaultIfEmpty()
                from homeTasks in _context.HomeTasks
                    .Where(hTs => hTs.LessonId == lessons.Id)
                    .DefaultIfEmpty()
                from homeWorks in _context.HomeWorks
                    .Where(hWr => hWr.HomeTaskId == homeTasks.Id && hWr.StudentId == studentId)
                    .DefaultIfEmpty()
                from teacher in _context.Users
                    .Where(t => t.Id == course.TeacherId && t.RoleId == (int)RoleEnum.Teacher)
                where course.Id == courseId
                select new
                {
                    Teacher = teacher,
                    Course = course,
                    Lesson = new DataModels.Lesson
                    {
                        Id = lessons.Id,
                        CourseId = lessons.CourseId,
                        Title = lessons.Title,
                        Description = lessons.Description,
                        Date = lessons.Date,
                        Material = lessons.Material,
                        Score = lessonScore != null ? lessonScore.Score : 0
                    },
                    HomeTask = homeTasks,
                    HomeWork = homeWorks
                })
                .ToListAsync();

            if (!coursesData.Any())
                return null;

            var firstRecord = coursesData.First();
            var domainCourse = await _courseFactory.CreateFrom(firstRecord.Course, firstRecord.Teacher);

            // Группируем сначала по урокам, затем по HomeTask, и собираем все HomeWork
            var lessonsWithTasks = coursesData
                .GroupBy(x => x.Lesson.Id)
                .Select(lessonGroup => new
                {
                    Lesson = lessonGroup.First().Lesson,
                    HomeTasks = lessonGroup
                        .Where(x => x.HomeTask != null)
                        .GroupBy(x => x.HomeTask.Id)
                        .Select(htGroup => new
                        {
                            HomeTask = htGroup.First().HomeTask,
                            HomeWorks = htGroup
                                .Where(x => x.HomeWork != null)
                                .Select(x => x.HomeWork)
                                .Distinct() // Убираем дубликаты, если есть
                                .ToList()
                        }).ToList()
                });

            foreach (var lessonGroup in lessonsWithTasks)
            {
                var domainLesson = await _lessonFactory.CreateAsync(lessonGroup.Lesson);

                // Добавляем HomeTask со всеми его HomeWorks
                foreach (var homeTaskGroup in lessonGroup.HomeTasks)
                {
                    if (homeTaskGroup.HomeTask != null)
                    {
                        var domainHomeTask = await _homeTaskFactory.CreateAsync(homeTaskGroup.HomeTask);

                        // Добавляем все HomeWorks для этого HomeTask
                        foreach (var homeWork in homeTaskGroup.HomeWorks)
                        {
                            var domainHomeWork = await _homeWorkFactory.CreateAsync(homeWork);
                            domainHomeTask.AddHomeWork(domainHomeWork);
                        }

                        domainLesson.HomeTask = domainHomeTask;
                    }
                }

                domainCourse.AddLesson(domainLesson);
            }

            return domainCourse;
        }

        public async Task<List<Student>> GetAllStudentsByCourse(int courseId)
        {
            var students = await (
                from sc in _context.StudentCourses
                where sc.CourseId == courseId
                join user in _context.Users on sc.StudentId equals user.Id
                select _studentFactory.CreateFrom(user)
            ).ToListAsync();

            return students;
        }

        public async Task<List<Student>> GetAllStudentsOutsideCourse(int courseId, int startRow, int endRow)
        {
            // Получаем Id студентов, которые уже находятся в курсе
            var enrolledStudentIds = await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .Select(sc => sc.StudentId)
                .ToListAsync();

            // Получаем всех студентов (Role = 2), исключая тех, кто уже в курсе
            var students = await _context.Users
                .Where(user => user.RoleId == 2 && !enrolledStudentIds.Contains(user.Id))
                .OrderBy(user => user.Id)  // Сортировка важна для корректной пагинации
                .Skip(startRow)
                .Take(endRow - startRow + 1)  // Пагинация
                .ToListAsync();

            // Преобразуем в Student модели
            var result = students.Select(_studentFactory.CreateFrom).ToList();
            return result;
        }


        public async Task AddStudentsToCourse(int courseId, int[] studentIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entries = studentIds.Select(id => new StudentCourse
                {
                    CourseId = courseId,
                    StudentId = id
                });

                _context.StudentCourses.AddRange(entries);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task RemoveStudentsFromCourse(int courseId, int[] studentIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entriesToRemove = await _context.StudentCourses
                    .Where(sc => sc.CourseId == courseId && studentIds.Contains(sc.StudentId))
                    .ToListAsync();

                _context.StudentCourses.RemoveRange(entriesToRemove);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> CheckIfUserInCourse(int userId, int courseId)
        {
            return await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == userId && sc.CourseId == courseId);
        }

        public async Task<List<int>> GetStudentIdsInCourse(int courseId, int[] studentIds)
        {
            return await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId && studentIds.Contains(sc.StudentId))
                .Select(sc => sc.StudentId)
                .ToListAsync();
        }

        public async Task<List<int>> GetStudentIdsNotInCourse(int courseId, int[] studentIds)
        {
            var enrolledStudentIds = await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId && studentIds.Contains(sc.StudentId))
                .Select(sc => sc.StudentId)
                .ToListAsync();

            return studentIds.Except(enrolledStudentIds).ToList();
        }

        public async Task<Score> GetCourseAverageScore(int courseId, int studentId)
        {
            var averageScore = await (
                from course in _context.Courses
                from lessons in _context.Lessons
                    .Where(l => l.CourseId == course.Id)
                from lessonScore in _context.LessonScores
                    .Where(lS => lS.LessonId == lessons.Id &&
                                lS.StudentId == studentId)
                where course.Id == courseId
                select (double?)lessonScore.Score
                )
                .AverageAsync();

            return new Score((int)Math.Round(averageScore ?? 0));
        }

        public async Task<Common.Domain.Entities.Lesson?> GetHomeworksInfo(int lessonId, int studentId)
        {
            var homeWorksData = await (
                from lesson in _context.Lessons.AsNoTracking()
                from homeTasks in _context.HomeTasks
                    .Where(hTs => hTs.LessonId == lessonId)
                                .ToList()
                    .DefaultIfEmpty()
                from homeWorks in _context.HomeWorks
                    .Where(hWr => hWr.HomeTaskId == homeTasks.Id &&
                                  hWr.StudentId == studentId)
                                .ToList()
                    .DefaultIfEmpty()
                where lesson.Id == lessonId
                select new
                {                    
                    homeWorks
                })
                .ToListAsync();

            return null; // homeWorksData;
        }
    }
}