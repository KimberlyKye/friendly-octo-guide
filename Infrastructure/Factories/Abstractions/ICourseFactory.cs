using Common.Domain.Entities;
using Common.Domain.ValueObjects;

namespace Infrastructure.Factories.Abstractions
{
    public interface ICourseFactory
    {
        public Task<Course> CreateFrom(DataModels.Course courseModel, DataModels.User teacher, Score? averageScore = null);

        public DataModels.Course MapTo(Course course);
    }
}