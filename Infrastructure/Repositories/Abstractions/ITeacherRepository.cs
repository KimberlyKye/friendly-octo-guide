using Dto.Teacher.Requests;
using Dto.Teacher.Responses;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository
    {
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);

    }
}
