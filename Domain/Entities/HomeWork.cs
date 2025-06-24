using Domain.Entities.Base;
using Domain.ValueObjects;
using ValueObjects;
using ValueObjects.Enums;
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
        public int Id { get; set; }
        public int HomeTaskId { get; set; }
        public int StudentId { get; set; }
        public Score Score { get; set; } = new Score(0);
        public TaskCompletionDate TaskCompletionDate { get; set; }
        public File Material { get; set; } // ссылка на файл
        public string StudentComment { get; set; }
        public string TeacherComment { get; set; }
        public HomeworkStatus Status { get; set; } = HomeworkStatus.Submitted;
        public bool IsOnTime { get; set; } // сдано вовремя

        /// <summary>
        /// Инициализирует новый экземпляр выполненной домашней работы
        /// </summary>
        /// <param name="homeTaskId">Задание, к которому относится работа (Id)</param>
        /// <param name="studentId">Студент, выполнивший работу (Id)</param>
        /// <param name="score">Оценка за работу (0-100)</param>
        /// <param name="taskCompletionDate">Дата выполнения</param>
        /// <param name="material">Файл с выполненым ДЗ </param>
        /// <param name="studentComment">Комментарий студента к попытке сдачи ДЗ</param>
        /// <param name="teacherComment">Комментарий преподавателя к попытке сдачи ДЗ</param>
        /// <param name="status">Статус дз - сдано или не сдано</param>
        /// <param name="isOnTime">Просрочено задание или нет</param>
        /// <exception cref="ArgumentNullException">
        /// Генерируется, если completionDate равно null
        /// </exception>
        public HomeWork(int homeTaskId, int studentId, Score score, TaskCompletionDate taskCompletionDate, File material, string studentComment, string teacherComment, HomeworkStatus status, bool isOnTime)
        : base(0)
        {
            HomeTaskId = homeTaskId;
            StudentId = studentId;
            Score = score;
            TaskCompletionDate = taskCompletionDate;
            Material = material;
            StudentComment = studentComment;
            TeacherComment = teacherComment;
            Status = status;
            IsOnTime = isOnTime;
        }

        /// <summary>
        /// Инициализирует новый экземпляр выполненной домашней работы
        /// </summary>
        /// <param name="id">Уникальный идентификатор работы</param>
        /// <param name="homeTaskId">Задание, к которому относится работа (Id)</param>
        /// <param name="studentId">Студент, выполнивший работу (Id)</param>
        /// <param name="score">Оценка за работу (0-100)</param>
        /// <param name="taskCompletionDate">Дата выполнения</param>
        /// <param name="material">Файл с выполненым ДЗ </param>
        /// <param name="studentComment">Комментарий студента к попытке сдачи ДЗ</param>
        /// <param name="teacherComment">Комментарий преподавателя к попытке сдачи ДЗ</param>
        /// <param name="status">Статус дз - сдано или не сдано</param>
        /// <param name="isOnTime">Просрочено задание или нет</param>
        /// <exception cref="ArgumentNullException">
        /// Генерируется, если completionDate равно null
        /// </exception>
        public HomeWork(int id, int homeTaskId, int studentId, Score score, TaskCompletionDate taskCompletionDate, File material, string studentComment, string teacherComment, HomeworkStatus status, bool isOnTime)
        : base(id)
        {
            Id = id;
            HomeTaskId = homeTaskId;
            StudentId = studentId;
            Score = score;
            TaskCompletionDate = taskCompletionDate;
            Material = material;
            StudentComment = studentComment;
            TeacherComment = teacherComment;
            Status = status;
            IsOnTime = isOnTime;
        }

        public HomeWork(int homeTaskId, int studentId, string? studentComment, TaskCompletionDate taskCompletionDate, HomeworkStatus submitted, bool isOnTime) : base(0)
        {
            HomeTaskId = homeTaskId;
            StudentId = studentId;
            StudentComment = studentComment;
            TaskCompletionDate = taskCompletionDate;
            // Submitted = submitted;
            IsOnTime = isOnTime;
        }

        // /// <summary>
        // /// Студент, выполнивший работу (только для чтения)
        // /// </summary>
        // public Student Student => _student;

        // /// <summary>
        // /// Задание, к которому относится работа (только для чтения)
        // /// </summary>
        // public HomeTask HomeTask => _homeTask;

        // /// <summary>
        // /// Оценка за работу (0-100)
        // /// </summary>
        // public Score Score
        // {
        //     get => _score;
        //     set => _score = value; // Score иммутабелен и всегда валиден
        // }

        // /// <summary>
        // /// Дата выполнения работы (только для чтения)
        // /// </summary>
        // public TaskCompletionDate CompletionDate => _completionDate;

        // /// <summary>
        // /// Прикрепленный материал (может быть null)
        // /// </summary>
        // public File? Material
        // {
        //     get => _material;
        //     set => _material = value;
        // }

        // /// <summary>
        // /// Комментарий студента (может быть null)
        // /// </summary>
        // public string? StudentComment
        // {
        //     get => _studentComment;
        //     set => _studentComment = value;
        // }

        // /// <summary>
        // /// Комментарий преподавателя (может быть null)
        // /// </summary>
        // public string? TeacherComment
        // {
        //     get => _teacherComment;
        //     set => _teacherComment = value;
        // }

        // /// <summary>
        // /// Проверяет, была ли работа сдана вовремя
        // /// </summary>
        // /// <returns>
        // /// true - если работа сдана в срок,
        // /// false - если работа сдана с опозданием
        // /// </returns>
        // public bool IsSubmittedOnTime()
        // {
        //     return _completionDate.Value <= _homeTask.Duration.EndDate;
        // }

        // /// <summary>
        // /// Возвращает процент выполнения задания
        // /// </summary>
        // public double CompletionPercentage => (double)_score.Value / Score.MaxValue * 100;

        // /// <summary>
        // /// Обновляет оценку за работу
        // /// </summary>
        // /// <param name="newScoreValue">Новое значение оценки (0-100)</param>
        // /// <exception cref="ArgumentOutOfRangeException">
        // /// Генерируется, если значение вне диапазона 0-100
        // /// </exception>
        // public void UpdateScore(int newScoreValue)
        // {
        //     _score = new Score(newScoreValue); // Валидация происходит в конструкторе Score
        // }

        // /// <summary>
        // /// Добавляет комментарий преподавателя
        // /// </summary>
        // /// <param name="comment">Текст комментария</param>
        // public void AddTeacherComment(string comment)
        // {
        //     TeacherComment = string.IsNullOrWhiteSpace(TeacherComment)
        //         ? comment
        //         : $"{TeacherComment}\n{comment}";
        // }
    }
}