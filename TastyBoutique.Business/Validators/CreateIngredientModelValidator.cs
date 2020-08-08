using FluentValidation;
using TastyBoutique.Business.Models.Ingredients;

namespace TastyBoutique.Business.Validators
{
    public sealed class CreateIngredientModelValidator : AbstractValidator<CreateIngredientModel>
    {
        public CreateIngredientModelValidator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }

    }
}
