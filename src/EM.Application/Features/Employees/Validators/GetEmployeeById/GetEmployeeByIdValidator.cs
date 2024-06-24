using EM.Application.Features.Employees.Queries.GetEmployeeById;
using FluentValidation;

namespace EM.Application.Features.Employees.Validators.GetEmployeeById
{
    public class GetEmployeeByIdValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdValidator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(1).WithMessage("Id is not valid!");
        }
    }
}
