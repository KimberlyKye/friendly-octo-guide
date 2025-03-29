using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;


namespace Domain.Entities
{
    public class Teacher : Person
    {
        public Teacher(int id, FullName name, PhoneNumber phoneNumber, Email email)
            : base(id, name, phoneNumber, email)
        {
        }

        public Teacher(FullName name, PhoneNumber phoneNumber, Email email)
            : base(0, name, phoneNumber, email)
        {
        }

        public void UpdateCourseInfo(Course course)
        {
            // Реализация метода
        }

        public void UpdateLessonInfo(Lesson lesson)
        {
            // Реализация метода
        }

        public void SetLessonScore(Student student, Lesson lesson)
        {
            // Реализация метода
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            // Реализация метода
        }

        public void CheckHomeWork(HomeWork homeWork, Score score, string comment)
        {
            // Реализация метода
        }

        public Teacher GetTeacher()
        {
            return this;
        }
    }

}