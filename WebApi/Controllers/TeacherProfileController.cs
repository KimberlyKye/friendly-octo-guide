using Application.Models.Teacher;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Teacher;
using WebApi.Dto.Teacher.Requests;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с профилем преподавателя
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherProfileController : Controller
    {
        private ITeacherProfileService _teacherProfileService;

        /// <summary>
        ///  Конструктор контроллера
        /// </summary>
        /// <param name="profileService"></param>
        public TeacherProfileController(
            ITeacherProfileService profileService
            )
        {
            _teacherProfileService = profileService;
        }

        /// <summary>
        /// Метод создания профиля преподавателя
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель созданного в БД пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /TeacherProfile
        ///     {
        ///        "email": "ivan.ivanov@email.com",
        ///        "password": "TestPass123",
        ///        "firstName": "Ivan",
        ///        "lastName": "Ivanov",
        ///        "birthDate": "1995-02-03",
        ///        "phoneNumber": "+79991112233"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Возвращает профиль созданного преподавателя</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateTeacherProfileAsync(CreateTeacherRequest request)
        {
            var newProfile = new CreateTeacherModel()
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber
            };

            var teacherProfile = await _teacherProfileService.CreateProfileAsync(newProfile);

            var res = new CreateTeacherResponse()
            {
                Id = teacherProfile.Id,
                Email = teacherProfile.Email,
                FirstName = teacherProfile.FirstName,
                LastName = teacherProfile.LastName,
                BirthDate = teacherProfile.BirthDate,
                PhoneNumber = teacherProfile.PhoneNumber
            };
            return StatusCode(201, res);

        }

        /// <summary>
        /// Метод получения информации в профиле преподавателя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Модель пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /TeacherProfile/1
        ///
        /// </remarks>
        /// <response code="200">Возвращает профиль преподавателя по его id</response>
        /// <response code="400">Если есть какие-то ошибки при получении данных</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTeacherProfileAsync(long id)
        {
            var teacherProfile = await _teacherProfileService.GetProfileInfoAsync(id);

            var res = new GetTeacherResponse()
            {
                Id = teacherProfile.Id,
                Email = teacherProfile.Email,
                FirstName = teacherProfile.FirstName,
                LastName = teacherProfile.LastName,
                BirthDate = teacherProfile.BirthDate,
                PhoneNumber = teacherProfile.PhoneNumber
            };
            return StatusCode(200, res);


        }

        /// <summary>
        /// Метод обновления информации в профиле преподавателя
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель обновленного в БД пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /TeacherProfile
        ///     {
        ///        "id": 1,
        ///        "email": "ivan.ivanov@email.com",
        ///        "firstName": "Ivan",
        ///        "lastName": "Ivanov",
        ///        "birthDate": "1995-02-03",
        ///        "phoneNumber": "+79991112233"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Возвращает профиль обновленного преподавателя</response>
        /// <response code="500">Если есть какие-то ошибки при обновлении</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTeacherProfileInfoAsync(UpdateTeacherRequest request)
        {
            var updatedProfile = new TeacherProfileModel()
            {
                Id = request.Id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                PhoneNumber = request.PhoneNumber
            };

            var teacherProfile = await _teacherProfileService.UpdateProfileInfoAsync(updatedProfile);

            var res = new UpdateTeacherResponse()
            {
                Id = teacherProfile.Id,
                Email = teacherProfile.Email,
                FirstName = teacherProfile.FirstName,
                LastName = teacherProfile.LastName,
                BirthDate = teacherProfile.BirthDate,
                PhoneNumber = teacherProfile.PhoneNumber
            };
            return StatusCode(200, res);

        }

    }
}
