using EM.Application.Features.Departments.Commands.CreateDepartment;
using FluentValidation;

namespace EM.Application.Features.Departments.Validators.CreateDepartment
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Department name is empty!");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Department name is too long!");
        }
    }
}
