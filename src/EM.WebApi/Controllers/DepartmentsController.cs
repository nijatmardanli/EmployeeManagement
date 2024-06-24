using EM.Application.Features.Departments.Commands.CreateDepartment;
using EM.Application.Features.Departments.Commands.DeleteDepartment;
using EM.Application.Features.Departments.Commands.UpdateDepartment;
using EM.Application.Features.Departments.Queries.FilterDepartment;
using EM.Application.Features.Departments.Queries.GetDepartmentById;
using EM.Application.Features.Departments.Queries.GetDepartmentList;
using Microsoft.AspNetCore.Mvc;

namespace EM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDepartmentByIdQuery() { Id = id }, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDepartmentListQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] FilterDepartmentQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDepartmentCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            _ = await Mediator.Send(new DeleteDepartmentCommand() { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
