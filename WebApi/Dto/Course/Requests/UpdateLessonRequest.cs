namespace WebApi.Dto.Course.Requests;
/// <summary>
/// Запрос на обновление урока
/// </summary>
public class UpdateLessonRequest
{
    /// <summary>
    /// Идентификатор урока.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }
    /// <summary>
    /// Название урока.
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Описание урока.
    /// </summary>
    /// <value></value>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Идентификатор курса.
    /// </summary>
    /// <value></value>
    public int CourseId { get; set; }
    /// <summary>
    /// Дата урока.
    /// </summary>
    /// <value></value>
    public DateTime Date { get; set; }
    /// <summary>
    /// Материал урока.
    /// </summary>
    /// <value></value>
    public string? Material { get; set; }

}
