using EM.Application.Features.Departments.Queries.GetDepartmentById;
using FluentValidation;

namespace EM.Application.Features.Departments.Validators.GetDepartmentById
{
    public class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdQuery>
    {
        public GetDepartmentByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id is not valid!");
        }
    }
}
