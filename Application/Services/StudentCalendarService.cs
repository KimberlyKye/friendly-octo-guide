using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;
using Application.Services.Abstractions;
using Entities;
using Infrastructure.Repositories;
using RepositoriesAbstractions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentCalendarService : IStudentCalendarService
    {
        private readonly IStudentCalendarRepository _studentCalendarRepository;
        private readonly IStudentInfoRepository _studentInfoRepository;

        public StudentCalendarService(
            IStudentCalendarRepository studentCalendarRepository,
            IStudentInfoRepository studentInfoRepository)
        {
            _studentCalendarRepository = studentCalendarRepository;
            _studentInfoRepository = studentInfoRepository;
        }

        public async Task<StudentCalendarResponseModel> GetPeriodCalendarData(GetStudentCalendarDataRequestModel request)
        {
            // Валидация входных параметров
            if (request.StartDate > request.EndDate)
            {
                throw new ArgumentException("Начальная дата не может быть позже конечной");
            }

            // Проверка существования студента
            Student? student = await _studentInfoRepository.GetStudentById(request.StudentId);
            if (student is null)
            {
                throw new ArgumentException($"Студент с ID {request.StudentId} не существует", nameof(request.StudentId));
            }

            // Получение данных из репозитория
            IReadOnlyCollection<Course> courses = await _studentCalendarRepository
                .GetPeriodCalendarData(request.StudentId, request.StartDate, request.EndDate);

            // Обработка случая отсутствия данных
            if (courses is null || !courses.Any())
            {
                return new StudentCalendarResponseModel
                {
                    CalendarLessonModels = Array.Empty<CalendarLessonModel>(),
                    CalendarHomeTaskModels = Array.Empty<CalendarHomeTaskModel>()
                };
            }

            // Формирование данных для ответа
            var lessonsList = new List<CalendarLessonModel>();
            var homeTasksList = new List<CalendarHomeTaskModel>();

            foreach (var course in courses)
            {
                // Обработка уроков курса
                foreach (var lesson in course.Lessons)
                {
                    lessonsList.Add(new CalendarLessonModel
                    {
                        CourseId = course.Id,
                        CourseName = course.Name.Value,
                        LessonId = lesson.Id,
                        LessonDate = lesson.Date,
                        LessonName = lesson.Name
                    });

                    // Обработка домашних заданий урока
                    if (lesson.HomeTasks != null)
                    {
                        foreach (var homeTask in lesson.HomeTasks)
                        {
                            homeTasksList.Add(new CalendarHomeTaskModel
                            {
                                CourseId = course.Id,
                                CourseName = course.Name.Value,
                                LessonId = lesson.Id,
                                LessonName = lesson.Name,
                                HomeTaskId = homeTask.Id,
                                HomeTaskName = homeTask.HomeTaskName.Value,
                                DateStart = homeTask.Duration.StartDate.ToDateTime(TimeOnly.MinValue),
                                DateEnd = homeTask.Duration.EndDate.ToDateTime(TimeOnly.MinValue)
                            });
                        }
                    }
                }
            }

            return new StudentCalendarResponseModel
            {
                CalendarLessonModels = lessonsList.ToArray(),
                CalendarHomeTaskModels = homeTasksList.ToArray()
            };
        }
    }
}
