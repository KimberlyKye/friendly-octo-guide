using Application.Models.Calendar.Requests;
using Application.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentCalendarService : IStudentCalendarService
    {
        public Task<int> GetCalendarData(GetStudentCalendarDataRequestModel teacherId)
        {
            throw new NotImplementedException();
        }
    }
}
