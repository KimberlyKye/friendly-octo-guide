using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class TeacherFactory : ITeacherFactory
    {
        public Task<Teacher> CreateFrom(User user)
        {
            return Task.FromResult(
                new Teacher(
                     user.Id,
                    new FullName(user.Name, user.Surname),
                    new PhoneNumber(user.PhoneNumber),
                    new Email(user.Email),
                    new BirthDate(DateOnly.FromDateTime((DateTime)user.DateOfBirth)),
                    user.Password
                    )
                );
        }

        public Task<User> CreateDataModelAsync(Teacher teacher)
        {
            var user = new User
            {
                RoleId = (int)RoleEnum.Teacher,
                Name = teacher.Name.FirstName,
                Surname = teacher.Name.LastName,
                Email = teacher.Email.Value,
                Password = teacher.Password,
                PhoneNumber = teacher.PhoneNumber,
                DateOfBirth = teacher.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue)
            };

            // “олько если teacher.Id имеет осмысленное значение
            if (teacher.Id > 0)
            {
                user.Id = teacher.Id;
            }

            return Task.FromResult(user);
        }
    }
}