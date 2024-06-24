using EM.Application.Features.Departments.Commands.UpdateDepartment;
using FluentValidation;

namespace EM.Application.Features.Departments.Validators.UpdateDepartment
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id is not valid!");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Department name is empty!");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Department name is too long!");
        }
    }
}
