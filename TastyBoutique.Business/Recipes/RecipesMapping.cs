using AutoMapper;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Models.RecipeComment;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Recipes
{
    public class RecipesMapping : Profile
    {
        public RecipesMapping()
        {
            CreateMap<UpsertRecipeModel, Persistance.Models.Recipes>();
            CreateMap<Persistance.Models.Recipes, RecipeModel>();

            CreateMap<CreateRecipeCommentModel,RecipeComment>();
            CreateMap<RecipeComment,RecipeCommentModel>();

        }
    }
}
