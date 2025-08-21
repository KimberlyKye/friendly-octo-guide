using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class HomeWorkConfiguration : IEntityTypeConfiguration<HomeWork>
    {
        public void Configure(EntityTypeBuilder<HomeWork> builder)
        {
            builder.ToTable("HomeWorks");

            builder.HasKey(hw => hw.Id);
            builder.Property(hw => hw.Id)
              .ValueGeneratedOnAdd(); // Автоинкремент

            builder.HasOne<StudentCourse>() поменять на User, создать конфиг для  HomeTask и миграцию откат-накат, перенести модели в Domain               .WithMany()
               .HasForeignKey(hw => hw.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<HomeTask>()
                .WithMany()
                .HasForeignKey(hw => hw.HomeTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(hw => hw.Score)
                .IsRequired() // Обязательное поле
                .HasDefaultValue(0); // Значение по умолчанию 0

            builder.Property(hw => hw.TaskCompletionDate)
                .IsRequired();

            builder.Property(hw => hw.Material)
                .IsRequired(false);

            builder.Property(hw => hw.StudentComment)
                .IsRequired();

            builder.Property(hw => hw.TeacherComment)
                .IsRequired();

            builder.HasIndex(hw => hw.StudentId);
            builder.HasIndex(hw => hw.HomeTaskId);
        }
    }
}
