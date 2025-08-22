using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Infrastructure.DataModels;
using Infrastructure.Factories.Abstractions;

namespace Infrastructure.Factories
{
    public class StudentFactory : IStudentFactory
    {
        public Task<Student> CreateFromAsync(User user)
        {
            return Task.FromResult(new Student(
                id: user.Id,
                name: new FullName(user.Name, user.Surname),
                phoneNumber: new PhoneNumber(user.PhoneNumber),
                email: new Email(user.Email),
                birthDate: new BirthDate((DateTime)user.DateOfBirth)));
        }

        public Task<User> CreateDataModelAsync(Student student)
        {
            return Task.FromResult(new User
            {
                Id = student.Id,
                RoleId = (int)RoleEnum.Student,
                Name = student.Name.FirstName,
                Surname = student.Name.LastName,
                Email = student.Email.Value,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth.Date.ToDateTime(TimeOnly.MinValue),
                Password = student.Password
            });
        }

    }
}