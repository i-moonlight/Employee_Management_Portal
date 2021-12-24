using FluentValidation;

namespace WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Validator for create employee command.
    /// </summary>
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeDto.Id)
                .Empty()
                .WithMessage("Identifier is excluded");

            RuleFor(cmd => cmd.EmployeeDto.Name)
                .NotNull().WithMessage("Name must be filled")
                .NotEmpty().WithMessage("Name must be filled")
                .MaximumLength(50).WithMessage("Name must be shorter");

            RuleFor(x => x.EmployeeDto.Department)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.EmployeeDto.DateOfJoining)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.EmployeeDto.PhotoFileName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}