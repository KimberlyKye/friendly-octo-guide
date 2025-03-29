using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Entities
{
    public class Course : Entity<int>
    {
        private int _state;

        private Teacher _teacher;

        private CourseName _courseName;

        private string _description;

        private Duration _duration;

        public List<Lesson> Lessons { get; }

        private List<Student> _students;
        //public Course(int id) : base(id);
        public Course(int id, Teacher teacher,
                      CourseName courseName,
                      string description,
                      Duration duration,
                      List<Lesson> lessons,
                      List<Student> students) : base(id)
        {
            _teacher = teacher;
            _courseName = courseName;
            _description = description;
            _duration = duration;
            Lessons = lessons;
            _students = students;
        }

        //public List<Lesson> GetLessonList()
        //{
        //    return _lessons;
        //}

        public Teacher GetTeacherInfo()
        {
            return _teacher;
        }

        public CourseName GetName()
        {
            return _courseName;
        }

        public string GetDescription()
        {
            return _description;
        }
        //public (Teacher teacher, CourseName courseName, string description, Duration duration, IReadOnlyList<Lesson> lessons, IReadOnlyList<Student> students) GetCourse()
        //{
        //    return (_teacher,
        //            _courseName,
        //            _description,
        //            _duration,
        //            _lessons.AsReadOnly(),
        //            _students.AsReadOnly());
        //}

    }
}
