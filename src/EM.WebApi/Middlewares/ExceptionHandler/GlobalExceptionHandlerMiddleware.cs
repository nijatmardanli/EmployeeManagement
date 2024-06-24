using EM.Application.Exceptions;
using System.Net;
using System.Text;
using ValidationException = FluentValidation.ValidationException;

namespace EM.WebApi.Middlewares.ExceptionHandler;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private LogLevel _logLevel;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            StringBuilder exceptionMessage = new(exception.Message);
            var innerException = exception.InnerException;
            string? stackTrace = exception.StackTrace;


            while (innerException != null)
            {
                exceptionMessage = exceptionMessage.Append($"{Environment.NewLine}{innerException.Message}");
                innerException = innerException.InnerException;
            }

            await HandleExceptionAsync(context, exception);
            logger.Log(_logLevel, exceptionMessage.Append($"{Environment.NewLine}{stackTrace}").ToString());
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        return exception switch
        {
            ValidationException => CreateValidationException(context, exception),
            BusinessException => CreateBusinessException(context, exception),
            NotFoundException => CreateNotFoundException(context, exception),
            _ => CreateInternalException(context, exception),
        };
    }

    #region Generating http responses according exception types
    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        _logLevel = LogLevel.Warning;

        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateNotFoundException(HttpContext context, Exception exception)
    {
        _logLevel = LogLevel.Warning;

        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);

        return context.Response.WriteAsync(new BusinessProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://example.com/probs/notfound",
            Title = "Not found exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        _logLevel = LogLevel.Warning;

        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        var errors = ((ValidationException)exception).Errors
            .GroupBy(er => er.PropertyName, er => er.ErrorMessage)
            .ToDictionary(er => er.Key,
                          er => er.ToList());

        return context.Response.WriteAsync(new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
        _logLevel = LogLevel.Critical;

        return context.Response.WriteAsync(new GeneralProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString()!);
    }
    #endregion
}