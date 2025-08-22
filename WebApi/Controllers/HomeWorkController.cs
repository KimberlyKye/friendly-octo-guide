using Application.Services.Abstractions;
using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Common.Domain.ValueObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.HomeWork.Requests;
using WebApi.Dto.HomeWork.Responses;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер для работы с домашними заданиями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HomeWorkController : ControllerBase
{
    /// <summary>
    /// Сервис для работы с домашними заданиями (попытки сдачи).
    /// </summary>
    private readonly IHomeWorkService _homeWorkService;

    /// <summary>
    /// Сервис для работы с домашними заданиями.
    /// </summary>
    private readonly IHomeTaskService _homeTaskService;

    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    private readonly IFileStorageService _fileStorageService;

    /// <summary>
    /// Конструктор контроллера для работы с домашними заданиями.
    /// </summary>
    /// <param name="homeWorkService"></param>
    /// <param name="fileStorageService"></param>
    /// <param name="homeTaskService"></param>
    public HomeWorkController(
        IHomeWorkService homeWorkService,
        IFileStorageService fileStorageService,
        IHomeTaskService homeTaskService)
    {
        _homeWorkService = homeWorkService;
        _homeTaskService = homeTaskService;
        _fileStorageService = fileStorageService;
    }

    /// <summary>
    /// Создание новой попытки сдачи домашнего задания.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <example>
    ///     POST /api/homework/submit
    /// {
    ///     "homeTaskId": 1,
    ///     "studentId": 1,
    ///     "studentComment": "Думаю, что это не так"
    ///  }
    ///  </example>
    [HttpPost("submit")]
    public async Task<IActionResult> SubmitHomework([FromForm] HomeWorkSubmissionRequest request)
    {
        var homeTask = await _homeTaskService.GetByIdAsync(request.HomeTaskId);
        if (homeTask == null)
        {
            return BadRequest("Home task not found");
        }

        var currentDate = new DateOnly();
        var isOnTime = homeTask.Duration.EndDate >= currentDate;

        // Валидация - хотя бы что-то одно должно быть
        if (string.IsNullOrWhiteSpace(request.StudentComment) && request.File == null)
        {
            return BadRequest("Either comment or file must be provided");
        }


        // Проверка на наличие уже оцененной работы
        var isGraded = await _homeWorkService.IsHomeworkGradedAsync(
            request.StudentId, request.HomeTaskId);

        if (isGraded)
        {
            return BadRequest("This homework has already been graded and cannot be resubmitted");
        }

        var submission = new HomeWork
        (
             request.HomeTaskId,
            request.StudentId,
            request.StudentComment,
             new TaskCompletionDate(new DateOnly()),
            HomeworkStatus.Submitted,
            isOnTime
        );

        if (request.File != null)
        {
            var material = await _fileStorageService.SaveFileAsync(
                request.File.OpenReadStream(), request.File.FileName);
            submission.Material = new Common.Domain.ValueObjects.File(material);
        }

        var result = await _homeWorkService.SubmitHomeworkAsync(submission);
        return Ok(MapToResponse(result));
    }

    /// <summary>
    /// Получение попытки сдачи домашнего задания по идентификатору.
    /// </summary>
    /// <param name="submissionId"></param>
    /// <returns></returns>
    /// <example>
    ///     GET /api/homework/1 
    /// </example>
    [HttpGet("{submissionId}")]
    public async Task<IActionResult> GetSubmission(int submissionId)
    {
        var submission = await _homeWorkService.GetSubmissionAsync(submissionId);
        if (submission == null)
        {
            return NotFound();
        }

        // TODO: проверка прав доступа

        return Ok(MapToResponse(submission));
    }

    /// <summary>
    /// Скачивание файла сдачи домашнего задания.
    /// </summary>
    /// <param name="submissionId"></param>
    /// <returns></returns>
    /// <example>
    ///     GET /api/homework/1/download
    /// </example>
    [HttpGet("{submissionId}/download")]
    public async Task<IActionResult> DownloadFile(int submissionId)
    {
        var submission = await _homeWorkService.GetSubmissionAsync(submissionId);

        if (submission == null || string.IsNullOrEmpty(submission.Material.GetFullPath()))
        {
            return NotFound();
        }

        // TODO: проверка прав доступа

        var stream = await _fileStorageService.GetFileAsync(submission.Material.GetFullPath());
        return File(stream, "application/octet-stream", Path.GetFileName(submission.Material.GetFullPath()));
    }


    /// <summary>
    /// Оценка домашнего задания.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <example>
    ///    POST /api/homework/grade
    /// {  
    ///    submissionId: 1,
    ///    score: 100,
    ///    teacherComment: "Great job!"
    ///  }
    /// </example>
    [HttpPost("grade")]
    // [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GradeHomework([FromBody] GradeHomeWorkRequest request)
    {
        var submission = await _homeWorkService.GetSubmissionAsync(request.SubmissionId);
        if (submission == null)
        {
            return NotFound();
        }

        if (submission.Status == HomeworkStatus.Graded || submission.Status == HomeworkStatus.Rejected)
        {
            return BadRequest("This homework has already been graded");
        }

        submission.Score = new Score(request.Score ?? 0);
        submission.TeacherComment = request.TeacherComment;
        submission.Status = request.Score.HasValue ? HomeworkStatus.Graded : HomeworkStatus.Rejected;

        var result = await _homeWorkService.UpdateSubmissionAsync(submission);
        return Ok(MapToResponse(result));
    }

    /// <summary>
    /// Маппинг сущности HomeWork на HomeWorkSubmissionResponse.
    /// </summary>
    /// <param name="submission"></param>
    /// <returns></returns>
    private HomeWorkSubmissionResponse MapToResponse(HomeWork submission)
    {
        return new HomeWorkSubmissionResponse
        {
            Id = submission.Id,
            HomeTaskId = submission.HomeTaskId,
            StudentId = submission.StudentId,
            Score = submission.Score,
            TaskCompletionDate = submission.TaskCompletionDate.Value,
            Material = submission.Material.GetFullPath(),
            StudentComment = submission.StudentComment,
            TeacherComment = submission.TeacherComment,
            Status = submission.Status.ToString()
        };
    }

}