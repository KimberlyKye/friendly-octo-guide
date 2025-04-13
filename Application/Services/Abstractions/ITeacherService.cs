﻿using Entities;
using Dto;

namespace Application.Services.Abstractions
{
    public interface ITeacherService
    {
        public Task<CalendarResponseDto> GetCalendarData(GetCalendarDataRequestDto requestDto);
    }
}
