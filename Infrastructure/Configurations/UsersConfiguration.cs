using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Настройка таблицы
            builder.ToTable("Users");

            // Первичный ключ
            builder.HasKey(c => c.Id);

            // Настройка свойств
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(c => c.Surname)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(c => c.DateOfBirth)
                .IsRequired()
                .HasColumnType("date"); // Для DateOnly в PostgreSQL

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.RoleId)
                .IsRequired();

            // Настройка связей

            // Связь с Role (один User - один Role)
            builder.HasOne<Role>()
                .WithMany()
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
                        
        }
    }
}
