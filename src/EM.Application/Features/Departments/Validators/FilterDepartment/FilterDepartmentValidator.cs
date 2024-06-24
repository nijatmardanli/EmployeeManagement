using EM.Application.Features.Departments.Queries.FilterDepartment;
using FluentValidation;

namespace EM.Application.Features.Departments.Validators.FilterDepartment
{
    public class FilterDepartmentValidator : AbstractValidator<FilterDepartmentQuery>
    {
        public FilterDepartmentValidator()
        {
            //RuleFor(x => x.Name).NotEmpty().WithMessage("Department name is empty!");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Department name is too short!");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("Department name is too long!");
        }
    }
}
