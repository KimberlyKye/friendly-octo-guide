using Domain.Entities.Base;
using Domain.ValueObjects;
using System.Collections.ObjectModel;
using File = Domain.ValueObjects.File;

namespace Entities
{
    /// <summary>
    /// Представляет домашнее задание, назначаемое студентам в рамках урока.
    /// </summary>
    /// <remarks>
    /// Класс инкапсулирует все атрибуты задания, включая метаданные (название, описание),
    /// временные параметры (срок выполнения), прикрепленные материалы и список сданных работ.
    /// Реализует паттерн частичной неизменяемости - основные поля можно изменять только
    /// через валидируемые сеттеры, а коллекция работ защищена от несанкционированного изменения.
    /// </remarks>
    public class HomeTask : Entity<int>
    {
        private HomeTaskName _homeTaskName;
        private string _description;
        private Duration _duration;
        private File? _material;
        private readonly List<HomeWork> _homeWorks = new();


        /// <summary>
        /// Инициализирует новый экземпляр домашнего задания
        /// </summary>
        /// <param name="id">Уникальный идентификатор задания в системе</param>
        /// <param name="homeTaskName">Объект названия задания</param>
        /// <param name="description">Подробное описание задания</param>
        /// <param name="duration">Временной интервал для выполнения</param>
        /// <param name="material">Прикрепленный учебный материал (опционально)</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает при попытке создать задание с пустым названием, описанием или нулевой длительностью
        /// </exception>
        public HomeTask(int id, HomeTaskName homeTaskName,
                      string description,
                      Duration duration,
                      File? material = null) : base(id)
        {
            _homeTaskName = homeTaskName ?? throw new ArgumentNullException(nameof(homeTaskName));
            _description = description ?? throw new ArgumentNullException(nameof(description));
            _duration = duration == default ? throw new ArgumentNullException(nameof(duration)) : duration;
            _material = material;
        }

        /// <summary>
        /// Название задания
        /// </summary>
        /// <value>
        /// Экземпляр <see cref="HomeTaskName"/>, гарантированно не-null.
        /// При установке значения выполняется проверка на null.
        /// </value>
        /// <exception cref="ArgumentNullException">При попытке установить null-значение</exception>
        public HomeTaskName HomeTaskName
        {
            get => _homeTaskName;
            set => _homeTaskName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Текстовое описание задания
        /// </summary>
        /// <value>
        /// Строка описания (не-null и не-пустая).
        /// При установке значения выполняется проверка на null.
        /// </value>
        /// <exception cref="ArgumentNullException">При попытке установить null-значение</exception>
        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Период времени на выполнение задания
        /// </summary>
        /// <value>
        /// Экземпляр <see cref="Duration"/>, гарантированно не-null и с валидными датами.
        /// При установке значения выполняется проверка на default (нулевое значение).
        /// </value>
        /// <exception cref="ArgumentNullException">При попытке установить default-значение</exception>
        public Duration Duration
        {
            get => _duration;
            set => _duration = value == default ? throw new ArgumentNullException(nameof(value)) : value;
        }

        /// <summary>
        /// Прикрепленный учебный материал
        /// </summary>
        /// <value>
        /// Экземпляр <see cref="File"/> или null, если материал не прикреплен.
        /// Допускает свободную установку значений, включая null.
        /// </value>
        public File? Material
        {
            get => _material;
            set => _material = value;
        }

        /// <summary>
        /// Коллекция сданных студенческих работ
        /// </summary>
        /// <value>
        /// Доступная только для чтения коллекция <see cref="HomeWork"/>.
        /// Для модификации коллекции используйте методы <see cref="AddHomeWork"/> и <see cref="RemoveHomeWork"/>.
        /// </value>
        public ReadOnlyCollection<HomeWork> HomeWorks => _homeWorks.AsReadOnly();

        public void AddHomeWork(HomeWork homeWork)
        {
            if (homeWork == null)
                throw new ArgumentNullException(nameof(homeWork));

            if (_homeWorks.Any(hw => hw.Id == homeWork.Id))
                throw new InvalidOperationException($"Работа с ID {homeWork.Id} уже существует в коллекции");

            _homeWorks.Add(homeWork);
        }

        /// <summary>
        /// Удаляет работу студента из коллекции сданных работ
        /// </summary>
        /// <param name="homeWork">Работа для удаления</param>
        /// <remarks>
        /// Если указанная работа не найдена в коллекции, метод завершается без ошибки.
        /// Для проверки существования работы перед удалением используйте LINQ-метод Any().
        /// </remarks>
        public void RemoveHomeWork(HomeWork homeWork)
        {
            _homeWorks.Remove(homeWork);
        }
    }
}