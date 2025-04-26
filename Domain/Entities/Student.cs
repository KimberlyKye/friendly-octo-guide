using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Entities
{
    public class Student : Person
    {
        public List<HomeWork> HomeWorks { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

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

        public void UpdateProfile(FullName fullName, PhoneNumber phoneNumber, Email email)
        {
            Validate(fullName, email, phoneNumber);
            FirstName = fullName.FirstName;
            LastName = fullName.LastName;
            Email = email.Value;
            PhoneNumber = phoneNumber;
        }

        private void Validate(FullName fullName, Email email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName.Name))
                throw new ArgumentException("Имя должно быть заполнено.");
            if (!IsValidEmail(email.Value))
                throw new ArgumentException("Некорректный адрес электронной почты.");
            if (!IsValidPhone(phoneNumber))
                throw new ArgumentException("Номер телефона некорректен.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phoneNumber)
        {
            const string pattern = @"^\+?[1-9]\d{1,14}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}