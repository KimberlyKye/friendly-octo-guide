using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Calendar.Requests
{
    public class GetTeacherCalendarDataRequestModel
    {
        public int TeacherId { get; set; }
        public DateOnly Date { get; set; }
    }
}
