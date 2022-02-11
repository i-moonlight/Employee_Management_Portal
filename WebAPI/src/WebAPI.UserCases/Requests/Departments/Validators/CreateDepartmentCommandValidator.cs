using FluentValidation;
using WebAPI.UserCases.Requests.Departments.Commands;
using static WebAPI.Utils.Constants.MessageTypes;

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
                .Empty().WithMessage(IdMustEmpty);

            RuleFor(cmd => cmd.DepartmentDto.Name)
                .NotNull().WithMessage(NameMustFilled)
                .NotEmpty().WithMessage(NameMustFilled)
                .MaximumLength(50).WithMessage(NameMustShorter);
        }
    }
}