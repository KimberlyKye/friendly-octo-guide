using Domain.Entities.Base;
using Domain.ValueObjects;
using ValueObjects;
using File = Domain.ValueObjects.File;


namespace Entities
{
    /// <summary>
    /// Представляет выполненную домашнюю работу студента
    /// </summary>
    /// <remarks>
    /// Содержит информацию о студенте, оценке, дате выполнения,
    /// прикрепленных материалах и комментариях преподавателя и студента.
    /// </remarks>
    public class HomeWork : Entity<int>
    {
        private readonly Student _student;
        private Score _score;
        private readonly TaskCompletionDate _completionDate;
        private File? _material;
        private string? _studentComment;
        private string? _teacherComment;
        private readonly HomeTask _homeTask;

        /// <summary>
        /// Инициализирует новый экземпляр выполненной домашней работы
        /// </summary>
        /// <param name="id">Уникальный идентификатор работы</param>
        /// <param name="student">Студент, выполнивший работу</param>
        /// <param name="homeTask">Задание, к которому относится работа</param>
        /// <param name="completionDate">Дата выполнения</param>
        /// <param name="score">Оценка за работу (0-100)</param>
        /// <exception cref="ArgumentNullException">
        /// Генерируется, если student, homeTask или completionDate равны null
        /// </exception>
        public HomeWork(int id,
                      Student student,
                      HomeTask homeTask,
                      TaskCompletionDate completionDate,
                      Score score) : base(id)
        {
            _student = student ?? throw new ArgumentNullException(nameof(student));
            _homeTask = homeTask ?? throw new ArgumentNullException(nameof(homeTask));
            _completionDate = completionDate ?? throw new ArgumentNullException(nameof(completionDate));
            _score = score; // Score уже валидируется в своем конструкторе
        }

        /// <summary>
        /// Студент, выполнивший работу (только для чтения)
        /// </summary>
        public Student Student => _student;

        /// <summary>
        /// Задание, к которому относится работа (только для чтения)
        /// </summary>
        public HomeTask HomeTask => _homeTask;

        /// <summary>
        /// Оценка за работу (0-100)
        /// </summary>
        public Score Score
        {
            get => _score;
            set => _score = value; // Score иммутабелен и всегда валиден
        }

        /// <summary>
        /// Дата выполнения работы (только для чтения)
        /// </summary>
        public TaskCompletionDate CompletionDate => _completionDate;

        /// <summary>
        /// Прикрепленный материал (может быть null)
        /// </summary>
        public File? Material
        {
            get => _material;
            set => _material = value;
        }

        /// <summary>
        /// Комментарий студента (может быть null)
        /// </summary>
        public string? StudentComment
        {
            get => _studentComment;
            set => _studentComment = value;
        }

        /// <summary>
        /// Комментарий преподавателя (может быть null)
        /// </summary>
        public string? TeacherComment
        {
            get => _teacherComment;
            set => _teacherComment = value;
        }

        /// <summary>
        /// Проверяет, была ли работа сдана вовремя
        /// </summary>
        /// <returns>
        /// true - если работа сдана в срок,
        /// false - если работа сдана с опозданием
        /// </returns>
        public bool IsSubmittedOnTime()
        {
            return _completionDate.Value <= _homeTask.Duration.EndDate;
        }

        /// <summary>
        /// Возвращает процент выполнения задания
        /// </summary>
        public double CompletionPercentage => (double)_score.Value / Score.MaxValue * 100;

        /// <summary>
        /// Обновляет оценку за работу
        /// </summary>
        /// <param name="newScoreValue">Новое значение оценки (0-100)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Генерируется, если значение вне диапазона 0-100
        /// </exception>
        public void UpdateScore(int newScoreValue)
        {
            _score = new Score(newScoreValue); // Валидация происходит в конструкторе Score
        }

        /// <summary>
        /// Добавляет комментарий преподавателя
        /// </summary>
        /// <param name="comment">Текст комментария</param>
        public void AddTeacherComment(string comment)
        {
            TeacherComment = string.IsNullOrWhiteSpace(TeacherComment)
                ? comment
                : $"{TeacherComment}\n{comment}";
        }
    }
}