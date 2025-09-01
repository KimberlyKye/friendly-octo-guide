using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("StudentCourses");

            builder.HasKey(sc => sc.Id);
            builder.Property(sc => sc.Id)
                  .ValueGeneratedOnAdd(); // Автоинкремент

            // Связь с User (Student)
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(sc => sc.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Связь с Course
            builder.HasOne<Course>()
                   .WithMany()
                   .HasForeignKey(sc => sc.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Индексы для производительности
            builder.HasIndex(sc => sc.StudentId);
            builder.HasIndex(sc => sc.CourseId);

            // Уникальный индекс, чтобы студент не мог быть записан на курс дважды
            builder.HasIndex(sc => new { sc.StudentId, sc.CourseId })
                   .IsUnique();
        }
    }
}
