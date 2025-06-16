using Domain.ValueObjects.Enums;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using Microsoft.EntityFrameworkCore;
using RepositoriesAbstractions.Abstractions;

namespace Infrastructure.Repositories
{
    public class TeacherProfileRepository : IUserProfileRepository<Entities.Teacher>
    {
        private readonly AppDbContext _context;
        private readonly ITeacherFactory _teacherFactory;

        public TeacherProfileRepository(AppDbContext context, ITeacherFactory teacherFactory)
        {
            _context = context;
            _teacherFactory = teacherFactory;
        }

        public async Task<Entities.Teacher> CreateUserProfileAsync(Entities.Teacher teacher)
        {
            // Преобразуем Teacher в User через фабрику
            var createdTeacher = await _teacherFactory.CreateDataModelAsync(teacher);

            // Сохраняем нового преподавателя в контексте
            _context.Users.Add(createdTeacher);
            await _context.SaveChangesAsync();

            return await _teacherFactory.CreateFrom(createdTeacher);
        }

        public async Task<Entities.Teacher?> GetUserProfileAsync(long teacherId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == teacherId && c.RoleId == (int)RoleEnum.Teacher);

            if (user == null)
            {
                return null;
            }

            return await _teacherFactory.CreateFrom(user);
        }

        public async Task<Entities.Teacher?> UpdateUserProfileAsync(Entities.Teacher updatedTeacher)
        {
            // Ищем текущего преподавателя по идентификатору
            var currentTeacher = await _context.Users.FirstOrDefaultAsync(c => c.Id == updatedTeacher.Id && c.RoleId == (int)RoleEnum.Teacher);

            if (currentTeacher == null)
            {
                return null;
            }

            var user = await _teacherFactory.CreateDataModelAsync(updatedTeacher);

            // Обновляем поля преподавателя
            currentTeacher.Name = user.Name;
            currentTeacher.Surname = user.Surname;
            currentTeacher.Email = user.Email;
            currentTeacher.PhoneNumber = user.PhoneNumber;
            currentTeacher.DateOfBirth = user.DateOfBirth;

            // Применяем изменения
            _context.Entry(currentTeacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _teacherFactory.CreateFrom(currentTeacher);
        }


        public async Task<bool> CheckUserByEmail(string email)
        {
            return await _context.Users
                          .AnyAsync(c => c.Email == email);
        }
    }
}
