using Application.Models.Student;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Student;
using WebApi.Dto.Student.Requests;

namespace WebApi.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с профилем студента
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentProfileController : Controller
    {
        //private readonly ILogger<StudentProfileController> _logger; // TODO: раскомментировать, когда добавим логгер в проект - https://github.com/KimberlyKye/friendly-octo-guide/issues/34
        private IStudentProfileService _studentProfileService;

        public StudentProfileController(
            //ILogger<StudentProfileController> logger, ; // TODO: раскомментировать, когда добавим логгер в проект - https://github.com/KimberlyKye/friendly-octo-guide/issues/34
            IStudentProfileService profileService
            )
        {
            //_logger = logger; ; // TODO: раскомментировать, когда добавим логгер в проект - https://github.com/KimberlyKye/friendly-octo-guide/issues/34
            _studentProfileService = profileService;
        }

        /// <summary>
        /// Метод создания профиля студента
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель созданного в БД пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /StudentProfile
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
        /// <response code="201">Возвращает профиль созданного студента</response>
        /// <response code="500">Если есть какие-то ошибки при создании</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateStudentProfileAsync(CreateStudentRequest request)
        {
            try
            {
                var newProfile = new CreateStudentModel()
                {
                    Email = request.Email,
                    Password = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    PhoneNumber = request.PhoneNumber
                };

                var studentProfile = await _studentProfileService.CreateProfileAsync(newProfile);

                var res = new CreateStudentResponse()
                {
                    Id = studentProfile.Id,
                    Email = studentProfile.Email,
                    FirstName = studentProfile.FirstName,
                    LastName = studentProfile.LastName,
                    BirthDate = studentProfile.BirthDate,
                    PhoneNumber = studentProfile.PhoneNumber
                };
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (можно добавить _logger)
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        /// <summary>
        /// Метод получения информации в профиле студента
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /StudentProfile/1
        ///
        /// </remarks>
        /// <response code="200">Возвращает профиль студента</response>
        /// <response code="400">Если есть какие-то ошибки при получении данных</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetStudentProfileAsync(long id)
        {
            try
            {
                var studentProfile = await _studentProfileService.GetProfileInfoAsync(id);

                var res = new GetStudentResponse()
                {
                    Id = studentProfile.Id,
                    Email = studentProfile.Email,
                    FirstName = studentProfile.FirstName,
                    LastName = studentProfile.LastName,
                    BirthDate = studentProfile.BirthDate,
                    PhoneNumber = studentProfile.PhoneNumber
                };
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (можно добавить _logger)
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        /// <summary>
        /// Метод обновления информации в профиле студента
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Модель обновленного в БД пользователя</returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /StudentProfile
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
        /// <response code="200">Возвращает профиль обновленного студента</response>
        /// <response code="500">Если есть какие-то ошибки при обновлении</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentProfileInfoAsync(UpdateStudentRequest request)
        {
            try
            {
                var updatedProfile = new StudentProfileModel()
                {
                    Id = request.Id,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    PhoneNumber = request.PhoneNumber
                };

                var studentProfile = await _studentProfileService.UpdateProfileInfoAsync(updatedProfile);

                var res = new UpdateStudentResponse()
                {
                    Id = studentProfile.Id,
                    Email = studentProfile.Email,
                    FirstName = studentProfile.FirstName,
                    LastName = studentProfile.LastName,
                    BirthDate = studentProfile.BirthDate,
                    PhoneNumber = studentProfile.PhoneNumber
                };
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (можно добавить _logger)
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

    }
}
