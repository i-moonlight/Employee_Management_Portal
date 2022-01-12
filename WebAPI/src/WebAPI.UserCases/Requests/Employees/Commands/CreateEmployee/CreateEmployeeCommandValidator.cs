using FluentValidation;

namespace WebAPI.UserCases.Requests.Employees.Commands.CreateEmployee
{
    /// <summary>
    /// Sets validation rules for create employee command.
    /// </summary>
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(cmd => cmd.EmployeeDto.Id)
                .Empty().WithMessage("Id must be empty");

            RuleFor(cmd => cmd.EmployeeDto.Name)
                .NotNull().WithMessage("Name must be filled")
                .NotEmpty().WithMessage("Name must be filled")
                .MaximumLength(50).WithMessage("Name must be shorter");

            RuleFor(cmd => cmd.EmployeeDto.Department)
                .NotEmpty().WithMessage("Department name must be filled")
                .MaximumLength(50).WithMessage("Department name must be shorter");

            RuleFor(cmd => cmd.EmployeeDto.DateOfJoining)
                .NotNull().WithMessage("Date must be filled")
                .NotEmpty().WithMessage("Date must be filled");

            RuleFor(cmd => cmd.EmployeeDto.PhotoFileName)
                .NotNull().WithMessage("Photo name must be filled")
                .NotEmpty().WithMessage("Photo name must be filled")
                .MaximumLength(50).WithMessage("Photo name must be shorter");
        }
    }
}