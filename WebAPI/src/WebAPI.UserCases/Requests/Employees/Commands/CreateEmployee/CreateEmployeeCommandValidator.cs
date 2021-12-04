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

            RuleFor(x => x.EmployeeDto.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

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