using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.DataModels;
using Entities;

namespace Domain.Factories.Abstractions
{ 
    public interface ITeacherFactory
    {
        public Teacher CreateFrom(User user);
    }
}