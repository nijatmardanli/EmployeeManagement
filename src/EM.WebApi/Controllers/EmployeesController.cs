using EM.Application.Features.Employees.Commands.CreateEmployee;
using EM.Application.Features.Employees.Commands.DeleteEmployee;
using EM.Application.Features.Employees.Commands.UpdateEmployee;
using EM.Application.Features.Employees.Queries.FilterEmployee;
using EM.Application.Features.Employees.Queries.GetEmployeeById;
using EM.Application.Features.Employees.Queries.GetEmployeeList;
using Microsoft.AspNetCore.Mvc;

namespace EM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetEmployeeByIdQuery() { Id = id }, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetEmployeeListQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] FilterEmployeeQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(new DeleteEmployeeCommand() { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
