using Common.Domain.Entities.Base;
using Common.Domain.ValueObjects;

namespace Common.Domain.Entities
{
    public class Person : Entity<int>
    {
        public FullName Name { get; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public BirthDate DateOfBirth { get; private set; }
        public List<Course> Courses { get; }
        public string Password { get; private set; }


        public Person(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate) : base(id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Courses = new List<Course>();
            DateOfBirth = birthDate;
        }

        public Person(int id, FullName name, PhoneNumber phoneNumber, Email email, BirthDate birthDate, string password) : base(id)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Courses = new List<Course>();
            DateOfBirth = birthDate;
            Password = password;
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