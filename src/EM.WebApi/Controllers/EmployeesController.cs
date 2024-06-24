using EM.Application.Features.Employees.Commands.CreateEmployee;
using Microsoft.AspNetCore.Mvc;

namespace EM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateEmployeeCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
    }
}
