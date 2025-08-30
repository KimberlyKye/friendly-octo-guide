using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;
using RepositoriesAbstractions.Abstractions;

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

            var lessonsTask = _studentCalendarRepository.GetLessonsAsync(request.StudentId, request.StartDate, request.EndDate);
            var homeTasksTask = _studentCalendarRepository.GetHomeTasksAsync(request.StudentId, request.StartDate, request.EndDate);

            await Task.WhenAll(lessonsTask, homeTasksTask);

            return new StudentCalendarResponseModel
            {
                CalendarLessonModels = await lessonsTask,
                CalendarHomeTaskModels = await homeTasksTask
            };            
        }
    }
}
