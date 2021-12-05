using FluentValidation;

namespace WebAPI.UserCases.Requests.Employees.Commands.UpdateEmployee
{
    /// <summary>
    /// Sets validation rules for update employee command.
    /// </summary>
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeDto.Id)
                .Empty()
                .WithMessage("Identifier is excluded");

            RuleFor(x => x.EmployeeDto.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.EmployeeDto.Department)
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