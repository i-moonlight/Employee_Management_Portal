using FluentValidation;
using WebAPI.UseCases.Requests.Employees.Commands;
using WebAPI.Utils.Constants;

namespace WebAPI.UseCases.Requests.Employees.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(cmd => cmd.EmployeeDto.Id)
                .Empty().WithMessage(ValidationTypes.IdMustEmpty);

            RuleFor(cmd => cmd.EmployeeDto.Name)
                .NotNull().WithMessage(ValidationTypes.NameMustFilled)
                .NotEmpty().WithMessage(ValidationTypes.NameMustFilled)
                .MaximumLength(50).WithMessage(ValidationTypes.NameMustShorter);

            RuleFor(cmd => cmd.EmployeeDto.Department)
                .NotEmpty().WithMessage(ValidationTypes.DepartmentNameMustFilled)
                .MaximumLength(50).WithMessage(ValidationTypes.DepartmentNameMustShorter);

            RuleFor(cmd => cmd.EmployeeDto.DateOfJoining)
                .NotNull().WithMessage(ValidationTypes.DateMustFilled)
                .NotEmpty().WithMessage(ValidationTypes.DateMustFilled);

            RuleFor(cmd => cmd.EmployeeDto.PhotoFileName)
                .NotNull().WithMessage(ValidationTypes.PhotoNameMustFilled)
                .NotEmpty().WithMessage(ValidationTypes.PhotoNameMustFilled)
                .MaximumLength(50).WithMessage(ValidationTypes.PhotoNameMustShorter);
        }
    }
}
