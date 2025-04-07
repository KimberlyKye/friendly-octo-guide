using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Infrastructure.DataModels;
using Infrastructure.Configurations;

namespace Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext(IConfiguration configuration)
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

            #endregion
        }
    }
}