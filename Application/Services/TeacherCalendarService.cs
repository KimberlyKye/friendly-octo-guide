using Application.Models.Calendar.Requests;
using Application.Models.Teacher.Responses;
using Application.Services.Abstractions;
using Infrastructure.Repositories;
using Domain.;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentCalendarService : ITeacherCalendarService
    {
        private readonly ITeacherCalendarRepository _teacherCalendarRepository;

        public StudentCalendarService(ITeacherCalendarRepository teacherCalendarRepository)
        {
            _teacherCalendarRepository = teacherCalendarRepository;
        }
        public async Task<TeacherCalendarResponseModel> GetWeekCalendarData(GetTeacherCalendarDataRequestModel request)
        {
            return await _teacherCalendarRepository.GetWeekCalendarData(request);
        }
    }
}
