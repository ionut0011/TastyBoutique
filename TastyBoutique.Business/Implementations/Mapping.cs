using AutoMapper;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.RecipeComment;
namespace TastyBoutique.Business.Recipes
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpsertRecipeModel, Persistance.Models.Recipes>();
            CreateMap<Persistance.Models.Recipes, TotalRecipeModel>();

            CreateMap<CreateIngredientModel, Persistance.Models.Ingredients>();
            CreateMap<Persistance.Models.Ingredients, IngredientModel>();
            CreateMap<IngredientModel, Ingredients>();

            CreateMap<CreateFilterModel, Persistance.Models.Filters>();
            CreateMap<Persistance.Models.Filters, FilterModel>();
            CreateMap<FilterModel, Filters>();

            CreateMap<Persistance.Models.Recipes, TotalRecipeModel>();

            CreateMap<SavedRecipes, SavedRecipeModel>();
            CreateMap<SavedRecipeModel, SavedRecipes>();

            CreateMap<RecipeComment, RecipeCommentModel>();
            CreateMap<CreateRecipeCommentModel, RecipeComment>();
        }
    }
}
