using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Abstractions;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 1. Инфраструктура
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("PgConnectionString")));
            services.AddScoped<ITeacherInfoRepository, TeacherInfoRepository>();
            services.AddScoped<ITeacherLessonRepository, TeacherLessonRepository>();
            services.AddScoped<ICourseInfoRepository, CourseInfoRepository>();

            // 2. Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
                        
            // 3. Основные сервисы приложения
            services.AddScoped<ITeacherLessonService, TeacherLessonService>();
            services.AddScoped<ITeacherInfoService, TeacherInfoService>();

            // 4. Фабрики
            services.AddTransient<IStudentFactory, StudentFactory>();
            services.AddTransient<ICourseFactory, CourseFactory>();
            services.AddTransient<ILessonFactory, LessonFactory>();
            services.AddTransient<IHomeTaskFactory, HomeTaskFactory>();
            services.AddTransient<ITeacherFactory, TeacherFactory>();
            services.AddTransient<IFileFactory, FileFactory>();
            
            // 5. MVC
            services.AddControllers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}