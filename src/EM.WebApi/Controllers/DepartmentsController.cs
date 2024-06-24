using EM.Application.Features.Departments.Commands.CreateDepartment;
using EM.Application.Features.Departments.Commands.UpdateDepartment;
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
    }
}
