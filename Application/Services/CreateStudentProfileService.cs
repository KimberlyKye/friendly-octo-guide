using Application.Services.Abstractions;
using Domain.ValueObjects;
using Dto;
using Entities;
using Infrastructure.Repositories.Abstractions;
using System.Globalization;

namespace Application.Services
{
    public class CreateStudentProfileService : ICreateStudentProfileService
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentProfileService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> Execute(CreateStudentProfileRequest request)
        {
            // Проверяем наличие существующих аккаунтов с такими данными
            if (await _studentRepository.ExistsWithEmailAsync(request.Email))
                throw new InvalidOperationException("Пользователь с данным email уже существует");

            if (await _studentRepository.ExistsWithPhoneAsync(request.PhoneNumber))
                throw new InvalidOperationException("Пользователь с указанным телефонным номером уже зарегистрирован");

            var birthDate = DateOnly.ParseExact(request.BirthDate,
                                         "MM/dd/yyyy hh:mm:ss tt",
                                         CultureInfo.InvariantCulture);
            var newProfile = new Student(new FullName(request.FirstName, request.LastName), new PhoneNumber(request.PhoneNumber), new Email(request.Email),
                                                                        new BirthDate(birthDate));
            await _studentRepository.CreateAsync(newProfile);
            return newProfile;
        }
    }
}