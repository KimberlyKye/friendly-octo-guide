using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Infrastructure.Factories.Abstractions
{
    public interface ITeacherFactory
    {
        public Teacher CreateFrom(DataModels.User user);
    }
}