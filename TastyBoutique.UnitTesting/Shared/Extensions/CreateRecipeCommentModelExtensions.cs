using TastyBoutique.Business.Models.RecipeComment;

namespace TastyBoutique.UnitTesting.Shared.Extensions
{
    public static class CreateRecipeCommentModelExtensions
    {

        public static CreateRecipeCommentModel WithComment(this CreateRecipeCommentModel model, string comment)
        {
            model.Comment = comment;
            return model;
        }

        public static CreateRecipeCommentModel WithReview(this CreateRecipeCommentModel model, int review)
        {
            model.Review = review;
            return model;
        }
    }
}
