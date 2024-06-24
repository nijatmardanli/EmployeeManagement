using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EM.WebApi.Middlewares.ExceptionHandler
{
    public class GeneralProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
