using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class HomeTaskConfiguration : IEntityTypeConfiguration<HomeTask>
    {
        public void Configure(EntityTypeBuilder<HomeTask> builder)
        {
            builder.ToTable("HomeTasks"); // имя таблицы в БД

            builder.HasKey(ht => ht.Id); // первичный ключ
            builder.Property(ht => ht.Id)
              .ValueGeneratedOnAdd(); // Автоинкремент

            builder.HasOne<Lesson>()              // Указываем тип связанной сущности (Lesson)
               .WithMany()                        // Указываем, что у Lesson много HomeTask (но без навигации)
               .HasForeignKey(ht => ht.LessonId)  // Внешний ключ в HomeTask
               .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление - удаляется HomeTask, если удаляется его Lesson

            builder.Property(ht => ht.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ht => ht.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(ht => ht.StartDate)
                .IsRequired();

            builder.Property(ht => ht.EndDate)
                .IsRequired();

            builder.Property(ht => ht.Material)
                .IsRequired(false); // допускаем NULL для Material

            builder.HasIndex(ht => ht.LessonId);    // Ускорит поиск по связи с Lesson
        }
    }
}
