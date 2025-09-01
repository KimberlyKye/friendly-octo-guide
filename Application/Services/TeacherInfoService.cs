using Application.Services.Abstractions;
using Common.Domain.Entities;
using RepositoriesAbstractions.Abstractions;

namespace Application.Services
{
    public class TeacherInfoService : ITeacherInfoService
    {
        private readonly ITeacherInfoRepository _teacherInfoRepository;

        public TeacherInfoService(ITeacherInfoRepository teacherInfoRepository)
        {
            _teacherInfoRepository = teacherInfoRepository;
        }
        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            Teacher? teacher = await _teacherInfoRepository.GetTeacherById(teacherId);
            if (teacher is null)
            {
                throw new ArgumentException($"Преподаватель с ID {teacherId} не существует", nameof(teacherId));
            }
            return teacher;
        }
    }
}
