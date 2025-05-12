
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto.Course.Requests;

/// <summary>
/// Запрос на создание курса
/// </summary>
public class CreateCourseRequest
{
    /// <summary>
    /// Состояние курса
    /// </summary>
    [Required(ErrorMessage = "Состояние обязательно")]
    public int StateId {  get; set; }

    /// <summary>
    /// Id учителя
    /// </summary>
    [Required(ErrorMessage = "Id учителя обязательно")]
    public int TeacherId { get; set; }

    /// <summary>
    /// Заголовок
    /// </summary>
    [Required(ErrorMessage = "Заголовок обязателен")]
    [MaxLength(50)]
    public string Title {  get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Дата начала
    /// </summary>
    [Required(ErrorMessage = "Дата начала курса")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания
    /// </summary>
    [Required(ErrorMessage = "Дата окончания курса")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateOnly EndDate { get; set;}

    /// <summary>
    /// PassingScore
    /// </summary>
    public int PassingScore { get; set; }
}