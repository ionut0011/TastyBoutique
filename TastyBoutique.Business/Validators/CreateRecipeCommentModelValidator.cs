using FluentValidation;
using TastyBoutique.Business.Models.RecipeComment;

namespace TastyBoutique.Business.Validators
{
    public sealed class CreateRecipeCommentModelValidator : AbstractValidator<CreateRecipeCommentModel>
    {
        public CreateRecipeCommentModelValidator()
        {
            RuleFor(model => model.Comment)
                .NotNull()
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);

            RuleFor(model => model.Review)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);

        }
    }
}
