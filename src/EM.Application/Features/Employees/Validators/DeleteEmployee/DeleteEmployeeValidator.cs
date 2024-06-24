using EM.Application.Features.Employees.Commands.DeleteEmployee;
using FluentValidation;

namespace EM.Application.Features.Employees.Validators.DeleteEmployee
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(1).WithMessage("Id is not valid!");
        }
    }
}
