using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;
using Application.Services.Abstractions;
using Dto;
using WebApi.Exceptions;

namespace WebApi.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICreateStudentProfileService _createService;
        private readonly IUpdateStudentProfileService _updateService;

        public StudentController(AppDbContext context, ICreateStudentProfileService createService,
        IUpdateStudentProfileService updateService)
        {
            _context = context;
            _createService = createService;
            _updateService = updateService;
        }

        /// <summary>
        /// Метод для создания профиля студента
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody][Bind("Id,RoleId,Name,Surname,Email,Password,PhoneNumber,DateOfBirth")] CreateStudentProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _createService.Execute(request);
                return CreatedAtAction(nameof(Create), new { id = response.Id }, request);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Метод для редактирования профиля студента
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody][Bind("Id,RoleId,Name,Surname,Email,Password,PhoneNumber,DateOfBirth")] UpdateStudentProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Id = request.Id;

            try
            {
                await _updateService.Execute(request);
                return NoContent(); // Код статуса 204 означает успешное обновление без возврата тела
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
