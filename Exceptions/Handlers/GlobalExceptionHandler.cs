using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebApi.Exceptions.Types;

namespace WebApi
{
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var errorResponse = new ApiErrorResponse()
            {
                Code = GetErrorCode(exception),
                Name = exception.GetType().Name,
                MessageHumanReadable = exception.Message,
                MessageTechnical = exception.InnerException?.Message ?? "",
                StackTrace = exception.StackTrace
            };

            // Логируем исключение
            _logger.LogError(exception, $"Ошибка: {exception.Message}");

            // Устанавливаем статус-коды и возвращаемый контент
            switch (context.Exception)
            {
                // case NotFoundException notFoundEx:
                //     context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                //     break;
                case InvalidOperationException invalidOpEx:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            context.Result = new JsonResult(errorResponse);
            base.OnException(context);
        }

        private static int GetErrorCode(Exception e)
        {
            // if (e is NotFoundException) return 404;
            // else 
            if (e is InvalidOperationException) return 409;
            else return 500;
        }
    }

}