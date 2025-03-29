using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.ValueObjects.Enums;

namespace Entities.Base
{
    public class Teacher : Person
    {
        protected Teacher(int id, string name, string email, DateTime birthDate, RoleEnum role) : base(id, name, email, birthDate, role)
        {
        }
    }
}
