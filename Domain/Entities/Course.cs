using Domain.Entities.Base;
using Domain.ValueObjects;
using Domain.ValueObjects.Enums;
using System.Collections.ObjectModel;

namespace Entities
{
    /// <summary>
    /// Представляет учебный курс в системе образования.
    /// </summary>
    /// <remarks>
    /// Класс инкапсулирует информацию о курсе, включая преподавателя, расписание,
    /// список уроков и зачисленных студентов, а также текущее состояние курса.
    /// </remarks>
    public class Course : Entity<int>
    {
        /// <summary>
        /// Текущее состояние курса (актуальный, архивный)
        /// </summary>
        public CourseState State { get; private set; }

        /// <summary>
        /// Преподаватель, ведущий курс
        /// </summary>
        public Teacher Teacher { get; private set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public CourseName Name { get; private set; }

        /// <summary>
        /// Описание содержания курса
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Продолжительность курса (начальная и конечная даты)
        /// </summary>
        public Duration Duration { get; private set; }

        /// <summary>
        /// Список уроков в курсе (только для чтения)
        /// </summary>
        public ReadOnlyCollection<Lesson> Lessons { get; private set; }
        private List<Lesson> _lessons = new();

        /// <summary>
        /// Список студентов, зачисленных на курс (только для чтения)
        /// </summary>
        public ReadOnlyCollection<Student> Students { get; private set; }
        private List<Student> _students = new();
        /// <summary>
        /// Минимальный проходной балл курса
        /// </summary>
        public Score _passingScore { get; private set; }
        /// <summary>
        /// Средний балл курса
        /// </summary>
        public Score AverageScore { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр курса
        /// </summary>
        /// <param name="id">Уникальный идентификатор курса</param>
        /// <param name="teacher">Преподаватель курса</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Продолжительность курса</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если teacher, courseName, description или duration не заданы</exception>
        public Course(int id,
                     Teacher teacher,
                     CourseName courseName,
                     string description,
                     Duration duration,                     
                     Score passingScore,
                     CourseState courseState = CourseState.Active) : base(id) //По умолчанию Активный
        {
            Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
            Name = courseName ?? throw new ArgumentNullException(nameof(courseName));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Duration = duration == default ? throw new ArgumentNullException(nameof(duration)) : duration;
            Lessons = _lessons.AsReadOnly();
            Students = _students.AsReadOnly();
            State = courseState;
            _passingScore = passingScore;
        }

        /// <summary>
        /// Добавляет урок в курс
        /// </summary>
        /// <param name="lesson">Урок для добавления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если lesson равен null</exception>
        public void AddLesson(Lesson lesson)
        {
            if (lesson == null) throw new ArgumentNullException(nameof(lesson));
            _lessons.Add(lesson);
        }
        /// <summary>
        /// Добавляет уроки в курс
        /// </summary>
        /// <param name="lessons">Уроки для добавления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если lesson равен null</exception>
        public void AddLessons(IEnumerable<Lesson> lessons)
        {
            if (lessons == null)
                throw new ArgumentNullException(nameof(lessons));

            foreach (var lesson in lessons)
            {
                AddLesson(lesson); // Используем существующую логику валидации
            }
        }

        /// <summary>
        /// Удаляет урок из курса
        /// </summary>
        /// <param name="lesson">Урок для удаления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если lesson равен null</exception>
        public void RemoveLesson(Lesson lesson)
        {
            if (lesson == null) throw new ArgumentNullException(nameof(lesson));
            _lessons.Remove(lesson);
        }

        /// <summary>
        /// Зачисляет студента на курс
        /// </summary>
        /// <param name="student">Студент для зачисления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если student равен null</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается, если студент уже зачислен</exception>
        public void EnrollStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (_students.Contains(student))
                throw new InvalidOperationException("Такой студент уже есть.");

            _students.Add(student);
        }

        /// <summary>
        /// Отчисляет студента с курса
        /// </summary>
        /// <param name="student">Студент для отчисления</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если student равен null</exception>
        public void RemoveStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            _students.Remove(student);
        }

        /// <summary>
        /// Изменяет состояние курса
        /// </summary>
        /// <param name="newState">Новое состояние курса</param>
        public void ChangeState(CourseState newState)
        {
            State = newState;
        }
    }
}