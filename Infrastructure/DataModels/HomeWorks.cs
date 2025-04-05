using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public class HomeWorks
    {
        public int Id { get; set; }
        public int HomeTaskId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public DateTime TaskCompletionDate { get; set; }
        public byte[] Material { get; set; }
        public string StudentComment { get; set; }
        public string TeacherComment { get; set;}
    }
}
