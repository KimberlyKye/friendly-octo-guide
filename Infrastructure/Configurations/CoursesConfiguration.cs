using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Настройка таблицы
            builder.ToTable("Courses");

            // Первичный ключ
            builder.HasKey(c => c.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd(); // Автоинкремент

            // Настройка свойств
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200); // Ограничение длины

            builder.Property(c => c.Description)
                .IsRequired();
                //.HasMaxLength(2000);  <========= Нужно ли ограничение????

            builder.Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("date"); // Для DateOnly в PostgreSQL

            builder.Property(c => c.EndDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.PassingScore)
                .IsRequired();
            //.HasDefaultValue(60); // Значение по умолчанию <========= Нужно ли ????

            // Настройка связей

            // Связь с State (один Course - один State)
            builder.HasOne<State>()
                .WithMany()
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с Teacher (один Course - один Teacher)
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы для улучшения производительности
            builder.HasIndex(c => c.StateId);
            builder.HasIndex(c => c.TeacherId);
        }
    }
}
