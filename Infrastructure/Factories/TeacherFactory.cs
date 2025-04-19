using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class TeacherFactory : ITeacherFactory
    {
        public Teacher CreateFrom(DataModels.User user)
        {

            var name = new FullName(user.Name, user.Surname);
            var phoneNumber = new PhoneNumber(user.PhoneNumber);
            var email = new Email(user.Email);
            var dateOfBirth = new BirthDate(new DateOnly()); // TODO: should be parsed

            return new Teacher(user.Id, name, phoneNumber, email, dateOfBirth);
        }
    }
}