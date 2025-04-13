using Dto;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ITeacherRepository
    {
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);

    }
}
