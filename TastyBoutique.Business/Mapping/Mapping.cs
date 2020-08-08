using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<UpsertRecipeModel, Persistance.Models.Recipes>();

            CreateMap<CreateIngredientModel, Ingredients>();
            CreateMap<Ingredients, IngredientModel>();


            CreateMap<CreateFilterModel, Filters>();
            CreateMap<Filters, FilterModel>();

            CreateMap<Persistance.Models.Recipes, TotalRecipeModel>()
            .ForMember(dest => dest.RecipesFilters, opt => opt.MapFrom(src => src.RecipesFilters))
            .ForMember(dest => dest.RecipesIngredients, opt => opt.MapFrom(src => src.RecipesIngredients.AsEnumerable()));

            CreateMap<SavedRecipes, SavedRecipeModel>();
            CreateMap<SavedRecipeModel, SavedRecipes>();

            CreateMap<RecipeComment, RecipeCommentModel>();
            CreateMap<CreateRecipeCommentModel, RecipeComment>();

            CreateMap<RecipesFilters, FilterModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Filter.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Filter.Id));
            CreateMap<RecipesIngredients, IngredientModel>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Ingredient.Id));
        }
    }
}
