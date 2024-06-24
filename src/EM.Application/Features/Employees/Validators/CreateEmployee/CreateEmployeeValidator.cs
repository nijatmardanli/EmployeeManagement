using EM.Application.Features.Employees.Commands.CreateEmployee;
using EM.CrossCuttingConcerns.DateTimes;
using FluentValidation;

namespace EM.Application.Features.Employees.Validators.CreateEmployee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Employee name is empty!");
            RuleFor(e => e.Name).MaximumLength(100).WithMessage("Employee name is too long!");

            RuleFor(e => e.LastName).NotEmpty().WithMessage("Employee last name is empty!");
            RuleFor(e => e.LastName).MaximumLength(100).WithMessage("Employee last name is too long!");

            DateTime minDate = IDateTimeProvider.DateNow.AddYears(-200);
            DateTime maxDate = IDateTimeProvider.DateNow.AddYears(-18);
            RuleFor(e => e.BirthDate).InclusiveBetween(minDate, maxDate).WithMessage("Employee birth date is not valid!");

            RuleFor(e => e.DepartmentId).GreaterThanOrEqualTo(1).WithMessage("Department id is not valid!");
        }
    }
}
