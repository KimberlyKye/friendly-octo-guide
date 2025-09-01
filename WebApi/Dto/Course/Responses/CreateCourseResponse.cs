namespace WebApi.Dto.Course.Responses;

public class CreateCourseResponse
{
    public int Id { get; set; }
    public int StateId { get; set; }
    public int TeacherId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int PassingScore { get; set; }
}