using AutoMapper;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpsertRecipeModel, Recipes>();
            CreateMap<Recipes, RecipeModel>();

            CreateMap<CreateIngredientModel, Ingredients>();
            CreateMap<Ingredients, IngredientModel>();

            CreateMap<CreateFilterModel, Filters>();
            CreateMap<Filters, FilterModel>();

            CreateMap<Recipes, TotalRecipeModel>();
            CreateMap<Recipes, RecipeModel>();

            CreateMap<SavedRecipes, SavedRecipeModel>();
            CreateMap<SavedRecipeModel, SavedRecipes>();

            CreateMap<RecipeComment, RecipeCommentModel>();
            CreateMap<CreateRecipeCommentModel, RecipeComment>();
        }
    }
}
