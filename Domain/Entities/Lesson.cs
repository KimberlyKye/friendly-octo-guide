using Domain.Entities.Base;
using Domain.ValueObjects;
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
        private readonly LessonName _lessonName;
        private string _description;
        private DateTime _date;
        private File? _material;
        private readonly List<HomeTask> _homeTasks = new();

        /// <summary>
        /// Инициализирует новый экземпляр занятия
        /// </summary>
        /// <param name="id">Уникальный идентификатор занятия</param>
        /// <param name="lessonName">Название занятия</param>
        /// <param name="description">Описание занятия</param>
        /// <param name="date">Дата и время проведения</param>
        /// <param name="material">Учебный материал (опционально)</param>
        /// <param name="homeTasks">Список домашних заданий</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает при передаче null для обязательных параметров
        /// </exception>
        public Lesson(int id,
                     LessonName lessonName,
                     string description,
                     DateTime date,
                     File? material = null,
                     IEnumerable<HomeTask>? homeTasks = null) : base(id)
        {
            _lessonName = lessonName ?? throw new ArgumentNullException(nameof(lessonName));
            _description = description ?? throw new ArgumentNullException(nameof(description));
            _date = date;
            _material = material;

            if (homeTasks != null)
            {
                _homeTasks.AddRange(homeTasks);
            }
        }

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

        /// <summary>
        /// Список домашних заданий (только для чтения)
        /// </summary>
        public ReadOnlyCollection<HomeTask> HomeTasks => _homeTasks.AsReadOnly();

        /// <summary>
        /// Добавляет домашнее задание к занятию
        /// </summary>
        /// <param name="homeTask">Домашнее задание для добавления</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает при попытке добавить null
        /// </exception>
        public void AddHomeTask(HomeTask homeTask)
        {
            if (homeTask == null) throw new ArgumentNullException(nameof(homeTask));
            _homeTasks.Add(homeTask);
        }

        /// <summary>
        /// Удаляет домашнее задание из занятия
        /// </summary>
        /// <param name="homeTask">Домашнее задание для удаления</param>
        /// <returns>
        /// true - если задание было успешно удалено,
        /// false - если задание не найдено в списке
        /// </returns>
        public bool RemoveHomeTask(HomeTask homeTask)
        {
            return _homeTasks.Remove(homeTask);
        }

        /// <summary>
        /// Проверяет, содержит ли занятие указанное домашнее задание
        /// </summary>
        public bool ContainsHomeTask(HomeTask homeTask)
        {
            return _homeTasks.Contains(homeTask);
        }
    }
}