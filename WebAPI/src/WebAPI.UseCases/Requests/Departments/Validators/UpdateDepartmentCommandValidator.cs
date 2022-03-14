﻿using FluentValidation;
using WebAPI.UseCases.Requests.Departments.Commands;
using static WebAPI.Utils.Constants.MessageTypes;

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
                .Empty().WithMessage(IdMustEmpty);

            RuleFor(cmd => cmd.DepartmentDto.Name)
                .NotNull().WithMessage(NameMustFilled)
                .NotEmpty().WithMessage(NameMustFilled)
                .MaximumLength(50).WithMessage(NameMustShorter);
        }
    }
}