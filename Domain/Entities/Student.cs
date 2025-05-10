using Domain.ValueObjects;
using System.ComponentModel;

namespace Entities
{
    public class Student : Person
    {
        public List<HomeWork> HomeWorks { get; private set; }

        public Student(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate)
            : base(id, name, phoneNumber, email, birthDate)
        {
            HomeWorks = new List<HomeWork>();
        }
        public Student(FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate)
            : base(0, name, phoneNumber, email, birthDate)
        {
            HomeWorks = new List<HomeWork>();
        }

        public Student(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate, string password)
            : base(id, name, phoneNumber, email, birthDate, password)
        {
            HomeWorks = new List<HomeWork>();
        }

        public Student(FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate, string password)
            : base(0, name, phoneNumber, email, birthDate, password)
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