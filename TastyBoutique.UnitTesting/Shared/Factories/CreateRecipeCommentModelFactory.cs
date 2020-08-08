using System;
using System.Collections.Generic;
using System.Text;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.UnitTesting.Shared.Extensions;

namespace TastyBoutique.UnitTesting.Shared.Factories
{
    public static class CreateRecipeCommentModelFactory
    {
        public static Guid IdReferinta= new Guid();

        public static CreateRecipeCommentModel Default()
        {
            return new CreateRecipeCommentModel() {Comment = "Foarte explicativ", IdUser = IdReferinta, Review = 4};
        }

        public static CreateRecipeCommentModel WithCommentLowerThan5Characters()
        {
            return CreateRecipeCommentModelExtensions.WithComment(Default(),"bun");
        }

        public static CreateRecipeCommentModel WithCommentGreaterThan250Characters()
        {
            return CreateRecipeCommentModelExtensions.WithComment(Default(),
                "Pvzg4bkGj3Ux95blXtscPOq1PDgbIXy5rYYoJyN2gl4LXE3gwKNYgyyw5QiUKEAv5lwogLN8GS8WvY6L0hszuiylfjKJ7IQuoUeIPLwkuOglo2Rh4HETduoHkYrZ3ShR1vmaUqiTlwncbUY3qRYpwLjsdTIejP5B7Ia6oxnyqW6hfaOxy9L9aG2x9nn4bHB7vxuX1VsqmYcFqnFkLdZrl4Fe15tZ5oae5mt6ewIBX2slv0d7AukggySCV1bZ");
        }


        public static CreateRecipeCommentModel WithCommentNull()
        {
            return CreateRecipeCommentModelExtensions.WithComment(Default(),
                null);
        }

        public static CreateRecipeCommentModel WithCommentEmpty()
        {
            return CreateRecipeCommentModelExtensions.WithComment(Default(),
                "");
        }

        public static CreateRecipeCommentModel WithNegativeReview()
        {
            return CreateRecipeCommentModelExtensions.WithReview(Default(), -1);
        }

        public static CreateRecipeCommentModel WithReviewGreaterThan5()
        {
            return CreateRecipeCommentModelExtensions.WithReview(Default(), 6);
        }

    }
}
