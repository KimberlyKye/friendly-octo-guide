

using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataModels
{
    public class State
    {
        public int Id { get; set; }
        [MaxLength(100)]  // Ограничение длины строки
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
