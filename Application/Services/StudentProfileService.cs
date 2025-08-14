using Application.Models.Student;
using Application.Services.Abstractions;
using Domain.ValueObjects;
using Entities;
using RepositoriesAbstractions.Abstractions;
using System.ComponentModel;

namespace Application.Services
{
    public class StudentProfileService : IStudentProfileService
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly IUserProfileRepository<Student> _studentProfileRepository;

        public StudentProfileService(IUserProfileRepository<Student> studentProfileRepository)
        {
            _studentProfileRepository = studentProfileRepository;
        }

        public async Task<StudentProfileModel> CreateProfileAsync(CreateStudentModel profileInfo)
        {
            // Добавляем semaphore чтобы избежать одновременного создания нескольких профилей с одинаковыми email
            await _semaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                // 1. Создание value-объектов
                var fullName = new FullName(profileInfo.FirstName, profileInfo.LastName);
                var phoneNumber = new PhoneNumber(profileInfo.PhoneNumber);
                var birthDate = new BirthDate(profileInfo.BirthDate);
                var email = new Email(profileInfo.Email);

                // 2. Проверка существующего пользователя
                var existingUser = await _studentProfileRepository
                    .CheckUserByEmail(profileInfo.Email)
                    .ConfigureAwait(false);

                if (existingUser)
                {
                    throw new ArgumentException("Профиль с таким email уже зарегистрирован!");
                }

                // 3. Создание студента
                var student = new Student(fullName, phoneNumber, email, birthDate, profileInfo.Password);
                var createdUser = await _studentProfileRepository
                    .CreateUserProfileAsync(student)
                    .ConfigureAwait(false);

                // 4. Маппинг результата
                return new StudentProfileModel
                {
                    Id = createdUser.Id,
                    FirstName = createdUser.GetName().FirstName,
                    LastName = createdUser.GetName().LastName,
                    Email = createdUser.Email.Value,
                    BirthDate = createdUser.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                    PhoneNumber = createdUser.GetPhoneNumber()
                };
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<StudentProfileModel> GetProfileInfoAsync(long profileId)
        {
            var user = await _studentProfileRepository.GetUserProfileAsync(profileId);

            var res = new StudentProfileModel()
            {
                Id = user.Id,
                FirstName = user.GetName().FirstName,
                LastName = user.GetName().LastName,
                Email = user.Email.Value,
                BirthDate = user.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                PhoneNumber = user.GetPhoneNumber()
            };

            return res;
        }

        public async Task<StudentProfileModel> UpdateProfileInfoAsync(StudentProfileModel profileInfo)
        {
            var fullName = new FullName(profileInfo.FirstName, profileInfo.LastName);
            var phoneNumber = new PhoneNumber(profileInfo.PhoneNumber);
            var birthDate = new BirthDate(profileInfo.BirthDate);
            var email = new Email(profileInfo.Email);

            var student = new Student(profileInfo.Id, fullName, phoneNumber, email, birthDate);

            var updatedUser = await _studentProfileRepository.UpdateUserProfileAsync(student);

            var res = new StudentProfileModel()
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.GetName().FirstName,
                LastName = updatedUser.GetName().LastName,
                Email = updatedUser.Email.Value,
                BirthDate = updatedUser.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                PhoneNumber = updatedUser.GetPhoneNumber()
            };

            return res;
        }

        public Task DeleteProfileAsync(long profileId)
        {
            throw new NotImplementedException();
        }
    }
}
