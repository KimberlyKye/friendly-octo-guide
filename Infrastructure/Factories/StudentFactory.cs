using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using Entities;
using Infrastructure.DataModels;
using Domain.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class StudentFactory : IStudentFactory
    {
        public Student CreateFrom(User userModel)
        {
            var birthDate = DateOnly.ParseExact(userModel.DateOfBirth.ToString(),
                                        "MM/dd/yyyy hh:mm:ss tt",
                                        CultureInfo.InvariantCulture);
            return new Student(
                id: userModel.Id,
                name: new FullName(userModel.Name, userModel.Surname),
                email: new Email(userModel.Email),
                phoneNumber: new PhoneNumber(userModel.PhoneNumber),
                birthDate: new BirthDate(birthDate)
            );
        }

        public User ConvertToUserModel(Student student)
        {
            // Преобразуем объект типа Student обратно в тип User
            string dateTimeString = $"{student.GetDateOfBirth().Value.Day:D2}/{student.GetDateOfBirth().Value.Month:D2}/{student.GetDateOfBirth().Value.Year:D4}";

            return new User()
            {
                Id = student.Id,
                Name = student.GetName().FirstName,
                Surname = student.GetName().LastName,
                Email = student.GetEmail().Value,
                PhoneNumber = student.GetPhoneNumber().Value,
                DateOfBirth = DateTime.Parse(dateTimeString + " 00:00:00")
            };
        }

    }
}