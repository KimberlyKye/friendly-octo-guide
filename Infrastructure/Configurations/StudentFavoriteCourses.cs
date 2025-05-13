using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class StudentFavoriteCoursesConfiguration : IEntityTypeConfiguration<StudentFavoriteCourse>
    {
        public void Configure(EntityTypeBuilder<StudentFavoriteCourse> builder)
        {
            // Настройка таблицы
            builder.ToTable("StudentFavoriteCourses");

            // Первичный ключ
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
              .ValueGeneratedOnAdd(); // Автоинкремент

            // Настройка свойств
            builder.Property(c => c.StudentId)
                .IsRequired();

            builder.Property(c => c.CourseId)
                .IsRequired();

            // Настройка связей

            builder.HasOne<Course>()
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.StudentId);

        }
    }
}
