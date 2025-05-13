namespace Application.Models.Course;

/// <summary>
/// Модель для создания курса
/// </summary>
public class CreateCourseModel
{
    /// <summary>
    /// Состояние курса
    /// </summary>
    public int StateId {  get; set; }

    /// <summary>
    /// Id учителя
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title {  get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateOnly EndDate { get; set;}

    /// <summary>
    /// PassingScore
    /// </summary>
    public int PassingScore { get; set; }
}