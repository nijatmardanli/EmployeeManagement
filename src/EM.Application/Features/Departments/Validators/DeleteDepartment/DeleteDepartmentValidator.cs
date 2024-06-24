using EM.Application.Features.Departments.Commands.DeleteDepartment;
using FluentValidation;

namespace EM.Application.Features.Departments.Validators.DeleteDepartment
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id is not valid!");
        }
    }
}
