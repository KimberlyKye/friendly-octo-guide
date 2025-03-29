using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Student : Person
    {
        public List<HomeWork> HomeWorks { get; private set; }

        public Student(int id, FullName name, PhoneNumber phoneNumber, Email email)
            : base(id, name, phoneNumber, email)
        {
            HomeWorks = new List<HomeWork>();
        }

        public Student(FullName name, PhoneNumber phoneNumber, Email email)
            : base(0, name, phoneNumber, email)
        {
            HomeWorks = new List<HomeWork>();
        }

        public void AddHomeWork(HomeTask homeTask, HomeWork homeWork)
        {
            HomeWorks.Add(homeWork);
        }

        public IReadOnlyList<HomeWork> GetListOfHomeWork()
        {
            return HomeWorks.ToList().AsReadOnly();
        }

        public Student GetStudent()
        {
            return this;
        }
    }

}