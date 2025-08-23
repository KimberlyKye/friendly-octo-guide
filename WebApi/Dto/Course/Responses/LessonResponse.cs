
namespace WebApi.Dto.Course.Responses;

/// <summary>
/// Занятие курса.
/// </summary>
public class LessonResponse
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    /// <value></value>
    public int Id { get; set; }
    /// <summary>
    /// Курс ID.
    /// </summary>
    /// <value></value>
    public int CourseId { get; set; }
    /// <summary>
    /// Название.
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Описание.
    /// </summary>
    /// <value></value>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Дата.
    /// </summary>
    /// <value></value>
    public DateTime Date { get; set; }
    /// <summary>
    /// Материал.
    /// </summary>
    /// <value></value>
    public string? MaterialName { get; set; }

    public LessonResponse(Entities.Lesson lesson)
    {
        this.Id = lesson.Id;
        this.CourseId = lesson.CourseId;
        this.Name = lesson.Name;
        this.Description = lesson.Description;
        this.Date = lesson.Date;
        if (lesson.Material is not null)
        {
            this.MaterialName = lesson.Material.Name;
        }
    }
}
