using FluentValidation;
using WebAPI.UserCases.Requests.Employees.Commands;
using static WebAPI.Utils.Constants.MessageTypes;

namespace WebAPI.UserCases.Requests.Employees.Validators
{
    /// <summary>
    /// Validator for create employee command.
    /// </summary>
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(cmd => cmd.EmployeeDto.Id)
                .Empty().WithMessage(IdMustEmpty);

            RuleFor(cmd => cmd.EmployeeDto.Name)
                .NotNull().WithMessage(NameMustFilled)
                .NotEmpty().WithMessage(NameMustFilled)
                .MaximumLength(50).WithMessage(NameMustShorter);

            RuleFor(cmd => cmd.EmployeeDto.Department)
                .NotEmpty().WithMessage(DepartmentNameMustFilled)
                .MaximumLength(50).WithMessage(DepartmentNameMustShorter);

            RuleFor(cmd => cmd.EmployeeDto.DateOfJoining)
                .NotNull().WithMessage(DateMustFilled)
                .NotEmpty().WithMessage(DateMustFilled);

            RuleFor(cmd => cmd.EmployeeDto.PhotoFileName)
                .NotNull().WithMessage(PhotoNameMustFilled)
                .NotEmpty().WithMessage(PhotoNameMustFilled)
                .MaximumLength(50).WithMessage(PhotoNameMustShorter);
        }
    }
}