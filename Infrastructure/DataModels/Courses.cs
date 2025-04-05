using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public class Courses
    {
        public int Id { get; set; }
        public int StateId {  get; set; }
        public int TeatcherId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}
        public int PassingScore { get; set; }
    }
}
