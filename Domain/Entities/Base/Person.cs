using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects.Enums;

namespace Domain.Entities.Base
{
    public class Person : Entity<int>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public RoleEnum Role { get; private set; }

        protected Person(int id, string name, string email, DateTime birthDate, RoleEnum role) : base(id)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.BirthDate = birthDate;
            this.Role = role;
        }
    }
}