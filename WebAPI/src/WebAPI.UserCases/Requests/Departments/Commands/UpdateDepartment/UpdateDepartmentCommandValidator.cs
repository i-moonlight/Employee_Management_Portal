using FluentValidation;

namespace WebAPI.UserCases.Requests.Departments.Commands.UpdateDepartment
{
    /// <summary>
    /// Sets validation rules for update department command.
    /// </summary>
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
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