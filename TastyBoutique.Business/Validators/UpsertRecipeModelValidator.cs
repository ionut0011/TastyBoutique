using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TastyBoutique.Business.Models.Recipe;

namespace TastyBoutique.Business.Validators
{
    public sealed class UpsertRecipeModelValidator : AbstractValidator<UpsertRecipeModel>
    {
        public UpsertRecipeModelValidator() {
            RuleFor(model => model.Name)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull();

            RuleFor(model => model.Description)
                .MaximumLength(300);

            RuleFor(model => model.Type)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(model => model.IngredientsList)
                .NotNull()
                .NotEmpty();

            RuleFor(model => model.Filter)
                .NotEmpty();
        }
    }
}
