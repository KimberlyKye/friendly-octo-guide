using Domain.Entities.Base;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using File = Domain.ValueObjects.File;

namespace Entities
{
    /// <summary>
    /// Представляет учебное занятие в рамках курса
    /// </summary>
    /// <remarks>
    /// Содержит информацию о занятии, включая название, описание, дату проведения,
    /// учебные материалы и список связанных домашних заданий.
    /// </remarks>
    public class Lesson : Entity<int>
    {
        private readonly int _courseId;
        private readonly LessonName _lessonName;
        private string _description;
        private DateTime _date;
        private File? _material;
        public HomeTask? HomeTask { get; set; }
        public Score? Score { get; private set; } 

        /// <summary>
        /// Инициализирует новый экземпляр занятия
        /// </summary>
        /// <param name="id">Уникальный идентификатор занятия</param>
        /// <param name="lessonName">Название занятия</param>
        /// <param name="description">Описание занятия</param>
        /// <param name="date">Дата и время проведения</param>
        /// <param name="material">Учебный материал (опционально)</param>
        /// <param name="homeTask">Список домашних заданий</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает при передаче null для обязательных параметров
        /// </exception>
        public Lesson(int id,
                     int courseId,
                     LessonName lessonName,
                     string description,
                     DateTime date,
                     File? material = null,                     
                     HomeTask? homeTask = null,
                     Score? score = null) : base(id)
        {
            _courseId = courseId;
            _lessonName = lessonName ?? throw new ArgumentNullException(nameof(lessonName));
            _description = description ?? throw new ArgumentNullException(nameof(description));
            _date = date;
            _material = material;
            HomeTask = homeTask;

            Score = score;
        }        

        public int CourseId => _courseId;
        /// <summary>
        /// Название занятия (только для чтения)
        /// </summary>
        public LessonName Name => _lessonName;

        /// <summary>
        /// Описание занятия
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Возникает при попытке установить null
        /// </exception>
        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Дата и время проведения занятия
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        /// <summary>
        /// Учебный материал (может быть null)
        /// </summary>
        public File? Material
        {
            get => _material;
            set => _material = value;
        }        
    }
}