using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        [MaxLength(100)]  // Ограничение длины строки
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public byte[] Material { get; set; }

    }
}
