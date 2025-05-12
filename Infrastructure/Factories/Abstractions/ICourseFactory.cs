using Entities;

namespace Infrastructure.Factories.Abstractions
{
    public interface ICourseFactory
    {
        public Task<Course> CreateFrom(DataModels.Course courseModel, DataModels.User teacher);

        public DataModels.Course MapTo(Course course);
    }
}