using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Factories.Abstractions
{
    public interface ICourseFactory
    {
        Task<Course> CreateFrom(DataModels.Course courseModel, DataModels.User teacher);
    }
}