using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Calendar.Requests
{
    public class GetTStudentCalendarDataRequestModel
    {
        public int StudentId { get; set; }
        public DateOnly Date { get; set; }
    }
}
