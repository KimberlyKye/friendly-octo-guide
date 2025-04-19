using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

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
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("PgConnectionString")));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers();

            services.AddTransient<IStudentFactory, StudentFactory>();
            services.AddTransient<ICourseFactory, CourseFactory>();
            services.AddTransient<ILessonFactory, LessonFactory>();
            services.AddTransient<IHomeTaskFactory, HomeTaskFactory>();
            services.AddTransient<ITeacherFactory, TeacherFactory>();
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