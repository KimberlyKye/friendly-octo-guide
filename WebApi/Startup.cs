using Application.Services;
using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.RepositoriesAbstractions;
using Common.RepositoriesAbstractions.Abstractions;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Infrastructure.Repositories;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using RepositoriesAbstractions.Abstractions;
using Serilog;
using System.Reflection;
using WebApi.Filters;
using WebApi.Middleware;

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
            services.AddCors();
            services.AddScoped<CustomExceptionFilter>();
            services.AddLogging();
            services.Configure<FileStorageOptions>(Configuration.GetSection("FileStorage"));


            // 1. DataBase context + repositories
            // Получаем строку подключения из переменных окружения (Render)
            var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // Если строка есть (деплой на Render), парсим её
            if (!string.IsNullOrEmpty(connectionUrl))
            {
                // Формат: postgresql://user:pass@host:5432/db
                var uri = new Uri(connectionUrl);
                var userInfo = uri.UserInfo.Split(':');

                var dbConnectionString = new NpgsqlConnectionStringBuilder
                {
                    Host = uri.Host,
                    Port = uri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = uri.AbsolutePath.TrimStart('/'),
                    SslMode = SslMode.Require,  // Для Render PostgreSQL
                    TrustServerCertificate = true
                }.ToString();

                // Переопределяем строку подключения
                Configuration["ConnectionStrings:PgConnectionString"] = dbConnectionString;
            }

            // Добавляем БД (EF Core)
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PgConnectionString")));

            services.AddScoped<ITeacherInfoRepository, TeacherInfoRepository>();
            services.AddScoped<IStudentInfoRepository, StudentInfoRepository>();
            services.AddScoped<ITeacherLessonRepository, TeacherLessonRepository>();
            services.AddScoped<ICourseInfoRepository, CourseInfoRepository>();
            services.AddScoped<IUserProfileRepository<Student>, StudentProfileRepository>();
            services.AddScoped<IUserProfileRepository<Teacher>, TeacherProfileRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherCalendarRepository, TeacherCalendarRepository>();
            services.AddScoped<IHomeWorkRepository, HomeWorkRepository>();
            services.AddScoped<IHomeTaskRepository, HomeTaskRepository>();
            services.AddScoped<IStudentCalendarRepository, StudentCalendarRepository>();
            services.AddScoped<IStudentInfoRepository, StudentInfoRepository>();

            // 2. Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Platform API",
                    Description = "ASP.NET Core Web API for learning management system named Platform. Created for Otus course C# Professional by Kurkin Kirill, Sharova Anastasia and Dudnikov Alexander. 2025",
                    Contact = new OpenApiContact
                    {
                        Name = "Our Git repository",
                        Url = new Uri("https://github.com/KimberlyKye/friendly-octo-guide")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Project description",
                        Url = new Uri("https://github.com/KimberlyKye/friendly-octo-guide/wiki")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

            // 3. Services
            services.AddScoped<ITeacherLessonService, TeacherLessonService>();
            services.AddScoped<ITeacherInfoService, TeacherInfoService>();
            services.AddScoped<IStudentInfoService, StudentInfoService>();
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<ITeacherProfileService, TeacherProfileService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITeacherCalendarService, TeacherCalendarService>();
            services.AddScoped<ITeacherProfileService, TeacherProfileService>();
            services.AddScoped<IHomeWorkService, HomeWorkService>();
            services.AddScoped<IHomeTaskService, HomeTaskService>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IStudentCalendarService, StudentCalendarService>();

            // 4. Factories
            services.AddTransient<IStudentFactory, StudentFactory>();
            services.AddTransient<ICourseFactory, CourseFactory>();
            services.AddTransient<ILessonFactory, LessonFactory>();
            services.AddTransient<IHomeTaskFactory, HomeTaskFactory>();
            services.AddTransient<ITeacherFactory, TeacherFactory>();
            services.AddTransient<IFileFactory, FileFactory>();
            services.AddTransient<IBaseFactory<Common.Domain.Entities.HomeWork, Infrastructure.DataModels.HomeWork>, HomeWorkFactory>();
            services.AddTransient<IHomeTaskFactory, HomeTaskFactory>();

            // 5. MVC

            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>(); // Фильтр обработки исключений
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging(); // Логирование HTTP-запросов


            app.UseMiddleware<LoggingMiddleware>(); // Использование кастомного мидлвара для логирования запросов в контроллерах. Если будут добавляться новые мидлвары, то можно будет их добавлять здесь, ниже LoggingMiddleware и выше UseCors. Порядок важен! Middleware выполняются сверху вниз.

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            // if (env.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            // }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
