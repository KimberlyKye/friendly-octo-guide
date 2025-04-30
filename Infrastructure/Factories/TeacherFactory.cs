using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.ValueObjects;
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

            return Task.FromResult(new Teacher(
                id: user.Id,
                name: new FullName(user.Name, user.Surname),
                phoneNumber: new PhoneNumber(user.PhoneNumber),
                email: new Email(user.Email),
                dateOfBirth: new BirthDate(DateOnly.FromDateTime((DateTime)user.DateOfBirth))));
        }

        public Task<User> CreateDataModelAsync(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));
            return Task.FromResult(new User
            {
                Id = teacher.Id,
                RoleId = 0,
                Name = teacher.Name.FirstName,
                Surname = teacher.Name.LastName,
                Email = teacher.Email.Value,
                //Password = teacher
                PhoneNumber = teacher.PhoneNumber,
                DateOfBirth = teacher.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue)
            });
        }
    }
}