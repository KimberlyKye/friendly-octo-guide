using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects.Base;

namespace Domain.ValueObjects
{
    public class Person : IValueObject
    {
        public int Id { get; private set; }
        public FullName Name { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BirthDate DateOfBirth { get; private set; }
        public List<Course> Courses { get; private set; }

        public Person(int id, FullName name, PhoneNumber phoneNumber, Email email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Courses = new List<Course>();
        }

        public FullName GetName()
        {
            return Name;
        }

        public PhoneNumber GetPhoneNumber()
        {
            return PhoneNumber;
        }

        public Email GetEmail()
        {
            return Email;
        }

        public BirthDate GetDateOfBirth()
        {
            return DateOfBirth;
        }

        public IReadOnlyList<Course> GetCourses()
        {
            return Courses.AsReadOnly();
        }

        public void SetCourse(Course course)
        {
            Courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            Courses.Remove(course);
        }


        public Person GetPerson()
        {
            return this;
        }

        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetEmail(Email email)
        {
            Email = email;
        }


        public void SetDateOfBirth(BirthDate dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

    }
}