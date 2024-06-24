using EM.Application.Features.Employees.Commands.CreateEmployee;
using EM.Application.Features.Employees.Commands.UpdateEmployee;
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

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeeCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
