﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public class HomeTask
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string Title { get; set; }        
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Material { get; set; }
    }
}
