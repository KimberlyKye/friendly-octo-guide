using Common.Domain.Entities;
namespace Application.Services.Abstractions
{
    public interface ITeacherInfoService
    {
        public Task<Teacher> GetTeacherById(int teacherId);
    }
}
