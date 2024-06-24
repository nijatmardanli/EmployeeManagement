using System.Text.Json;

namespace EM.WebApi.Middlewares.ExceptionHandler;

public class ValidationProblemDetails : GeneralProblemDetails
{
    public object Errors { get; set; } = null!;
    public override string ToString() => JsonSerializer.Serialize(this);
}