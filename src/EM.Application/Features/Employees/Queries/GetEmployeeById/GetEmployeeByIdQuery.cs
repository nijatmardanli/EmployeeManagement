using EM.Application.Features.Employees.Dtos;
using MediatR;

namespace EM.Application.Features.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery : IRequest<GetEmployeeDto>
    {
        public int Id { get; set; }
    }
}
