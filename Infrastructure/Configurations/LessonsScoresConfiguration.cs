using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class LessonsScoresConfiguration : IEntityTypeConfiguration<LessonScore>
    {
        public void Configure(EntityTypeBuilder<LessonScore> builder)
        {
            // Настройка таблицы
            builder.ToTable("LessonScores");

            // Первичный ключ
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
              .ValueGeneratedOnAdd(); // Автоинкремент

            // Настройка свойств
            builder.Property(c => c.Score)
                .IsRequired();

            builder.Property(c => c.StudentId)
                .IsRequired();

            builder.Property(c => c.LessonId)
                .IsRequired();

            // Настройка связей

            builder.HasOne<Lesson>()
                .WithMany()
                .HasForeignKey(c => c.LessonId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.StudentId);

        }
    }
}
