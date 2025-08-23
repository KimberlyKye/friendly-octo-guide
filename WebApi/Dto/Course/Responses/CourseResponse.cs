namespace WebApi.Dto.Course.Responses;
/// <summary>
/// Модель курса для ответа API.
/// </summary>
public class CourseResponse
{
    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }
    /// <summary>
    /// Состояние курса.
    /// </summary>
    /// <value></value>
    public int StateId { get; set; }
    /// <summary>
    /// Идентификатор учителя.
    /// </summary>
    /// <value></value>
    public int TeacherId { get; set; }
    /// <summary>
    /// Название курса.
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Описание курса.
    /// </summary>
    /// <value></value>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Дата начала курса.
    /// </summary>
    /// <value></value>
    public DateOnly StartDate { get; set; }
    /// <summary>
    /// Дата окончания курса.
    /// </summary>
    /// <value></value>
    public DateOnly EndDate { get; set; }
}
