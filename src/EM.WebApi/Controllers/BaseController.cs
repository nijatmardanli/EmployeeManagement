using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EM.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
