using Application.Models.Teacher;
using Application.Services.Abstractions;
using Domain.ValueObjects;
using Entities;
using Infrastructure.Repositories.Abstractions;
using System.ComponentModel;

namespace Application.Services
{
    public class TeacherProfileService : ITeacherProfileService
    {
        private readonly IUserProfileRepository<Teacher> _teacherProfileRepository;

        public TeacherProfileService(IUserProfileRepository<Teacher> teacherProfileRepository)
        {
            _teacherProfileRepository = teacherProfileRepository;
        }

        public async Task<TeacherProfileModel> CreateProfileAsync(CreateTeacherModel profileInfo)
        {
            var fullName = new FullName(profileInfo.FirstName, profileInfo.LastName);
            var phoneNumber = new PhoneNumber(profileInfo.PhoneNumber);
            var birthDate = new BirthDate(profileInfo.BirthDate);
            var email = new Email(profileInfo.Email);

            var teacher = new Teacher(fullName, phoneNumber, email, birthDate, profileInfo.Password);

            var existingUser = await _teacherProfileRepository.CheckUserByEmail(profileInfo.Email);

            if (existingUser)
            {
                throw new ArgumentException("Профиль с таким email уже зарегестрирован!");
            }

            var createdUser = await _teacherProfileRepository.CreateUserProfileAsync(teacher);

            var res = new TeacherProfileModel()
            {
                Id = createdUser.Id,
                FirstName = createdUser.GetName().FirstName,
                LastName = createdUser.GetName().LastName,
                Email = createdUser.Email.Value,
                BirthDate = createdUser.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                PhoneNumber = createdUser.GetPhoneNumber()
            };

            return res;
        }

        public async Task<TeacherProfileModel> GetProfileInfoAsync(long profileId)
        {
            var user = await _teacherProfileRepository.GetUserProfileAsync(profileId);

            var res = new TeacherProfileModel()
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

        public async Task<TeacherProfileModel> UpdateProfileInfoAsync(TeacherProfileModel profileInfo)
        {
            var fullName = new FullName(profileInfo.FirstName, profileInfo.LastName);
            var phoneNumber = new PhoneNumber(profileInfo.PhoneNumber);
            var birthDate = new BirthDate(profileInfo.BirthDate);
            var email = new Email(profileInfo.Email);

            var teacher = new Teacher(profileInfo.Id, fullName, phoneNumber, email, birthDate);

            var updatedUser = await _teacherProfileRepository.UpdateUserProfileAsync(teacher);

            var res = new TeacherProfileModel()
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
