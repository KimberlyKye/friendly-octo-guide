using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            // Настройка таблицы
            builder.ToTable("Lessons");

            // Первичный ключ
            builder.HasKey(l => l.Id);

            // Настройка полей
            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(200); // соответствует атрибуту [MaxLength(200)]

            builder.Property(l => l.Description)
                .IsRequired();

            builder.Property(l => l.Date)
                .IsRequired();

            builder.Property(l => l.Material)
                .IsRequired(false); // допускаем NULL для Material
            
            builder.HasOne<Course>()              // Указываем тип связанной сущности (Course)
               .WithMany()                        // Указываем, что у Course много Lesson (но без навигации)
               .HasForeignKey(l => l.CourseId)    // Внешний ключ в Lesson
               .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление - удаляется Lesson, если удаляется его Course

            builder.HasIndex(l => l.CourseId);      // Ускорит поиск по связи с Course
        }
    }
}
