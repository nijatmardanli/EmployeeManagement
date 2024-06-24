using EM.Application.Features.Departments.Queries.GetDepartmentList;
using Microsoft.AspNetCore.Mvc;

namespace EM.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDepartmentListQuery(), cancellationToken);

            return Ok(result);
        }
    }
}
