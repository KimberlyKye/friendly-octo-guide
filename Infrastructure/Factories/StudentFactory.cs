using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class StudentFactory : IStudentFactory
    {
        public Student CreateFrom(User userModel)
        {
            return new Student(
                id: userModel.Id,
                name: new FullName(userModel.Name, userModel.Surname),
                email: new Email(userModel.Email),
                phoneNumber: new PhoneNumber(userModel.PhoneNumber),
                birthDate: null //new BirthDate(new DateOnly(userModel.DateOfBirth.ToString))
            );
        }
    }
}