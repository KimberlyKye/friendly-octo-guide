using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using Entities;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class TeacherFactory : ITeacherFactory
    {
        public Task<Teacher> CreateFrom(User user)
        {
            if (user is null
                || user.DateOfBirth is null)
                throw new ArgumentNullException(nameof(user));

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
            if (teacher is null)
                throw new ArgumentNullException(nameof(teacher));

            return Task.FromResult(new User
            {
                Id = teacher.Id,
                RoleId = (int)RoleEnum.Teacher,
                Name = teacher.Name.FirstName,
                Surname = teacher.Name.LastName,
                Email = teacher.Email.Value,
                Password = teacher.Password,
                PhoneNumber = teacher.PhoneNumber,
                DateOfBirth = teacher.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue)
            });
        }
    }
}