using Application.Services.Abstractions;
using Domain.ValueObjects;
using Dto;
using Infrastructure.Repositories.Abstractions;
using WebApi.Exceptions;

namespace Application.Services
{
    public class UpdateStudentProfileService : IUpdateStudentProfileService
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentProfileService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task Execute(UpdateStudentProfileRequest request)
        {
            var existingProfile = await _studentRepository.GetByIdAsync(request.Id);

            if (existingProfile == null)
                throw new NotFoundException($"Профиль студента с идентификатором '{request.Id}' не найден");

            // Проверьте уникальность новых значений, если меняются почта или номер телефона
            if (request.Email != existingProfile.Email && await _studentRepository.ExistsWithEmailAsync(request.Email))
                throw new InvalidOperationException("Пользователь с таким email уже существует");

            if (request.PhoneNumber != existingProfile.PhoneNumber && await _studentRepository.ExistsWithPhoneAsync(request.PhoneNumber))
                throw new InvalidOperationException("Пользователь с указанным телефонным номером уже зарегистрирован");

            existingProfile.UpdateProfile(new FullName(request.FirstName, request.LastName), new PhoneNumber(request.PhoneNumber), new Email(request.Email));
            await _studentRepository.UpdateAsync(existingProfile);
        }
    }
}