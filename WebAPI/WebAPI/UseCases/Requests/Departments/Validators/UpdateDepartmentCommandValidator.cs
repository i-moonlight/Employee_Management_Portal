using FluentValidation;
using WebAPI.UseCases.Requests.Departments.Commands;
using WebAPI.Utils.Constants;

namespace WebAPI.UseCases.Requests.Departments.Validators
{
    /// <summary>
    /// Validator for update department command.
    /// </summary>
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(cmd => cmd.DepartmentDto.Id)
                .Empty().WithMessage(ValidationTypes.IdMustEmpty);

            RuleFor(cmd => cmd.DepartmentDto.Name)
                .NotNull().WithMessage(ValidationTypes.NameMustFilled)
                .NotEmpty().WithMessage(ValidationTypes.NameMustFilled)
                .MaximumLength(50).WithMessage(ValidationTypes.NameMustShorter);
        }
    }
}
