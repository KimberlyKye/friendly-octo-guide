using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.Models.Calendar.Requests;
using Common.Models.Calendar.Responses;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services
{
    public class TeacherCalendarService : ITeacherCalendarService
    {
        private readonly ITeacherCalendarRepository _teacherCalendarRepository;
        private readonly ITeacherInfoRepository _teacherInfoRepository;

        public TeacherCalendarService(
            ITeacherCalendarRepository teacherCalendarRepository,
            ITeacherInfoRepository teacherInfoRepository)
        {
            _teacherCalendarRepository = teacherCalendarRepository;
            _teacherInfoRepository = teacherInfoRepository;
        }
        public async Task<TeacherCalendarResponseModel> GetPeriodCalendarData(GetTeacherCalendarDataRequestModel request)
        {
            var minDate = new DateOnly(2000, 1, 1);

            if (request.StartDate > request.EndDate)
            {
                throw new ArgumentException("Начальная дата не может быть позже конечной");
            }

            Teacher? teacher = await _teacherInfoRepository.GetTeacherById(request.TeacherId);
            if (teacher is null)
            {
                throw new ArgumentNullException($"Преподаватель с ID {request.TeacherId} не существует", nameof(request.TeacherId));
            }

            return await _teacherCalendarRepository.GetPeriodCalendarData(request.TeacherId, request.StartDate, request.EndDate);
        }
    }
}