using FluentValidation;
using WebAPI.UserCases.Requests.Departments.Commands;

namespace WebAPI.UserCases.Requests.Departments.Validators
{
    /// <summary>
    /// Validator for create department command.
    /// </summary>
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(cmd => cmd.DepartmentDto.Id)
                .Empty().WithMessage("Id must be empty");

            RuleFor(cmd => cmd.DepartmentDto.Name)
                .NotNull().WithMessage("Name must be filled")
                .NotEmpty().WithMessage("Name must be filled")
                .MaximumLength(50).WithMessage("Name must be shorter");
        }
    }
}