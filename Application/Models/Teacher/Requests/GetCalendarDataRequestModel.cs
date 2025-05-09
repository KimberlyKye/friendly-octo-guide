using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.ValueObjects.File;

namespace Application.Models.Teacher.Requests
{
    public class GetCalendarDataRequestModel
    {
        public int UserId { get; set; }
        public DateOnly Date { get; set; }
    }
}
