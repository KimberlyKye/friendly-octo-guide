using Domain.ValueObjects;
using Entities;

namespace Infrastructure.Factories.Abstractions
{
    public interface ICourseFactory
    {
        public Task<Course> CreateFrom(DataModels.Course courseModel, DataModels.User teacher, Score? averageScore = null);

        public DataModels.Course MapTo(Course course);
    }
}