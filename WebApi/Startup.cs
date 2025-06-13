using Application.Services.Abstractions;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Repositories;
using Entities;
using System.Reflection;
using Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Middleware;
using WebApi.Filters;
using RepositoriesAbstractions.Abstractions;

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

            // 1. ��������������
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(Configuration.GetConnectionString("PgConnectionString")));
            services.AddScoped<ITeacherInfoRepository, TeacherInfoRepository>();
            services.AddScoped<ITeacherLessonRepository, TeacherLessonRepository>();
            services.AddScoped<ICourseInfoRepository, CourseInfoRepository>();
            services.AddScoped<IUserProfileRepository<Student>, StudentProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // 2. Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Platform API",
                    Description = "ASP.NET Core Web API  ��� ���������� ���������. ��������� (Platform) - ��� ������� ��� �������������� �������� ��������� (Student).",
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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter token in format 'bearer<space>token'"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

            });

            // 3. �������� ������� ����������
            services.AddScoped<ITeacherLessonService, TeacherLessonService>();
            services.AddScoped<ITeacherInfoService, TeacherInfoService>();
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITeacherCalendarService, TeacherCalendarService>();

            // 4. �������
            services.AddTransient<IStudentFactory, StudentFactory>();
            services.AddTransient<ICourseFactory, CourseFactory>();
            services.AddTransient<ILessonFactory, LessonFactory>();
            services.AddTransient<IHomeTaskFactory, HomeTaskFactory>();
            services.AddTransient<ITeacherFactory, TeacherFactory>();
            services.AddTransient<IFileFactory, FileFactory>();

            // 5. MVC
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };

            });

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

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}