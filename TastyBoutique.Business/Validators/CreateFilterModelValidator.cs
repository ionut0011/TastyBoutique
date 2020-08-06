using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.Filter;

namespace TastyBoutique.Business.Validators
{
    public sealed class CreateFilterModelValidator : AbstractValidator<CreateFilterModel>
    {
        public CreateFilterModelValidator()
        {
            RuleFor(model => model.Name)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();
        }
    }
}
