using Infrastructure.Configurations;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        #region тут модельки Кирилла (задача 19)
        public DbSet<Course> Courses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<HomeTask> HomeTasks { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        #endregion

        #region тут модельки Насти (задача 20)
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentFavoriteCourse> StudentFavoriteCourses { get; set; }
        public DbSet<LessonScore> LessonScores { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region настройки таблиц Кирилла с использованием  Fluent API (вынести в отдельную папку, например Configuration и реализовывать интерфейс IEntityTypeConfiguration)
            modelBuilder
                .ApplyConfiguration(new CoursesConfiguration())
                .ApplyConfiguration(new LessonConfiguration())
                .ApplyConfiguration(new HomeTaskConfiguration())
                .ApplyConfiguration(new HomeWorkConfiguration());


            #endregion

            #region настройки таблиц Насти с использованием  Fluent API (вынести в отдельную папку, например Configuration и реализовывать интерфейс IEntityTypeConfiguration)
            modelBuilder
                .ApplyConfiguration(new UsersConfiguration())
                .ApplyConfiguration(new StudentFavoriteCoursesConfiguration())
                .ApplyConfiguration(new LessonsScoresConfiguration());
            #endregion

            // Seed-данные для таблицы ролей
            modelBuilder.Entity<Role>().HasData(
            [
            new Role {Id = 1, Name = "Teacher", Description="Преподаватель"},
            new Role {Id = 2, Name = "Student", Description = "Студент"}
            ]);

            // Seed-данные для таблицы состояний курсов
            modelBuilder.Entity<State>().HasData(
            [
            new State {Id = 1, Name = "Archive", Description= "Завершенный курс, который ушел в архив"},
            new State {Id = 2, Name = "Active", Description= "Активный курс"}
            ]);
        }
    }
}