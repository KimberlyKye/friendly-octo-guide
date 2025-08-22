namespace WebApi.Dto.Course.Requests;
/// <summary>
/// Модель запроса для обновления курса.
/// </summary>
public class UpdateCourseRequest
{
    /// <summary>
    /// Id курса.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Id состояния.
    /// </summary>
    public int StateId { get; set; }
    /// <summary>
    /// Id учителя.
    /// </summary>
    /// <value></value>
    public int TeacherId { get; set; }
    /// <summary>
    /// Заголовок.
    /// </summary>
    /// <value></value>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Описание.
    /// </summary>
    /// <value></value>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Дата начала.
    /// </summary>
    /// <value></value>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Дата окончания.
    /// </summary>
    /// <value></value>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// Балл прохождения.
    /// </summary>
    /// <value></value>
    public int PassingScore { get; set; }
}
