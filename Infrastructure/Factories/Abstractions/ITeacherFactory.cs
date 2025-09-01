using Common.Domain.Entities;
using Infrastructure.DataModels;

namespace Infrastructure.Factories.Abstractions
{
    public interface ITeacherFactory
    {
        public Task<Teacher> CreateFrom(User user);
        public Task<User> CreateDataModelAsync(Teacher teacher);
    }
}