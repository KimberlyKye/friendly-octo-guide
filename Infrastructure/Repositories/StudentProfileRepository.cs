using Domain.ValueObjects.Enums;
using Infrastructure.Contexts;
using Infrastructure.Factories.Abstractions;
using RepositoriesAbstractions.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentProfileRepository : IUserProfileRepository<Entities.Student>
    {
        private readonly AppDbContext _context;
        private readonly IStudentFactory _studentFactory;

        public StudentProfileRepository(AppDbContext context, IStudentFactory studentFactory)
        {
            _context = context;
            _studentFactory = studentFactory;
        }

        public async Task<Entities.Student> CreateUserProfileAsync(Entities.Student student)
        {
            // Преобразуем Student в User через фабрику
            var createdStudent = await _studentFactory.CreateDataModelAsync(student);

            // Сохраняем нового студента в контексте
            _context.Users.Add(createdStudent);
            await _context.SaveChangesAsync();

            return await _studentFactory.CreateFromAsync(createdStudent);
        }

        public async Task<Entities.Student> GetUserProfileAsync(long studentId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == studentId && c.RoleId == (int)RoleEnum.Student);

            if (user == null)
            {
                throw new InvalidOperationException("Профиль студента не найден.");
            }

            return await _studentFactory.CreateFromAsync(user);
        }

        public async Task<Entities.Student> UpdateUserProfileAsync(Entities.Student updatedStudent)
        {
            // Ищем текущего студента по идентификатору
            var currentStudent = await _context.Users.FirstOrDefaultAsync(c => c.Id == updatedStudent.Id && c.RoleId == (int)RoleEnum.Student);

            if (currentStudent == null)
            {
                throw new InvalidOperationException("Профиль студента не найден.");
            }

            var user = await _studentFactory.CreateDataModelAsync(updatedStudent);

            // Обновляем поля студента
            currentStudent.Name = user.Name;
            currentStudent.Surname = user.Surname;
            currentStudent.Email = user.Email;
            currentStudent.PhoneNumber = user.PhoneNumber;
            currentStudent.DateOfBirth = user.DateOfBirth;

            // Применяем изменения
            _context.Entry(currentStudent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _studentFactory.CreateFromAsync(currentStudent);
        }


        public async Task<bool> CheckUserByEmail(string email)
        {
            return await _context.Users
                          .AnyAsync(c => c.Email == email);
        }
    }
}
