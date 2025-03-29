using Domain.Entities.Base;
using Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Base
{
    public class Student : Person
    {
        protected Student(int id, string name, string email, DateTime birthDate, RoleEnum role) : base(id, name, email, birthDate, role)
        {
        }
    }
}
