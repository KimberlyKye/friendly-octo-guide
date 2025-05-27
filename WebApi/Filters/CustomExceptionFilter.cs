using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

/// <summary>
/// Класс для обработки исключений в приложении
/// </summary>
public class CustomExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Информация о среде выполнения приложения
    /// </summary>
    private readonly IHostEnvironment _env;
    /// <summary>
    /// Логгер для записи ошибок в журнал
    /// </summary>
    private readonly ILogger<CustomExceptionFilter> _logger;

    /// <summary>
    ///  Конструктор класса, принимает объект IHostEnvironment для получения информации о среде выполнения приложения и объект ILogger для логирования ошибок.
    /// </summary>
    /// <param name="env"></param>
    /// <param name="logger"></param>
    public CustomExceptionFilter(IHostEnvironment env, ILogger<CustomExceptionFilter> logger)
    {
        _env = env;
        _logger = logger;
    }

    /// <summary>
    /// Метод вызывается при возникновении исключения в контроллерах и сервисах.
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        // Получаем исключение
        var ex = context.Exception;

        // Генерируем уникальный ID ошибки
        var errorId = Guid.NewGuid().ToString();

        // Определяем сообщение и код статуса ошибки
        var message = ex.Message;
        var statusCode = StatusCodes.Status500InternalServerError;

        // Если нужно обрабатывать различные типы исключений по-разному, можно использовать конструкцию switch-case
        switch (ex)
        {
            // Обработка исключения NullReferenceException
            case NullReferenceException _:
                message = "Исключение типа NullReferenceException";
                break;

            // Обработка исключения InvalidOperationException
            case InvalidOperationException invalidOpEx:
                message = $"Некорректная операция: {invalidOpEx.Message}";
                statusCode = StatusCodes.Status400BadRequest;
                break;

            // Обработка исключения ArgumentException
            case ArgumentException argEx:
                message = $"Некорректный аргумент: {argEx.ParamName}";
                statusCode = StatusCodes.Status400BadRequest;
                break;

            // Обработка исключения ValidationException
            case ValidationException validationEx:
                message = validationEx.Message ?? "Ошибка валидации";
                statusCode = StatusCodes.Status400BadRequest;
                break;

            // Обработка исключения UnauthorizedAccessException
            case UnauthorizedAccessException _:
                message = "Доступ запрещён";
                statusCode = StatusCodes.Status401Unauthorized;
                break;

            // Обработка других исключений
            default:
                message ??= "Неизвестная ошибка";
                break;
        }

        // Объект, содержащий сформированный ответ на ошибку
        var error = new
        {
            Id = errorId,
            Type = ex.GetType().Name,
            StatusCode = statusCode,
            Message = message,
            Timestamp = DateTime.UtcNow.ToString("O"),
            Endpoint = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}",
            Details = _env.IsDevelopment() ? ex.Message : null,
            StackTrace = _env.IsDevelopment() ? ex.StackTrace : null,
            InnerError = ex.InnerException != null ? new { Message = ex.InnerException.Message } : null,
        };

        // Записываем информацию об ошибке в журнал с уровнем ошибки Error
        if (!_env.IsDevelopment())
        {
            _logger.LogError($"Информация об ошибке: {error}");
            _logger.LogError($"Детали ошибки: {ex.Message}");
            _logger.LogError($"Стек ошибки: {ex.StackTrace}");
        }
        else
        {
            _logger.LogError(ex, $"Ошибка: {error}");
        }

        // Отправляем ответ с ошибкой в формате JSON
        context.Result = new ObjectResult(error)
        {
            StatusCode = statusCode
        };

        // Обозначаем, что исключение было обработано и не нужно передавать его дальше
        context.ExceptionHandled = true;
    }
}
