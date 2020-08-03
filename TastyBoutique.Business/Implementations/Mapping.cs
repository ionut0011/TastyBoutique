using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Implementations.Models.Recipe;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Models.RecipeComment;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Recipes
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UpsertRecipeModel, Persistance.Models.Recipes>();
            CreateMap<Persistance.Models.Recipes, RecipeModel>();

            CreateMap<CreateIngredientModel, Persistance.Models.Ingredients>();
            CreateMap<Persistance.Models.Ingredients, IngredientModel>();

            CreateMap<CreateFilterModel, Persistance.Models.Filters>();
            CreateMap<Persistance.Models.Filters, FilterModel>();

            CreateMap<Persistance.Models.Recipes, TotalRecipeModel>();
            CreateMap<Persistance.Models.Recipes, RecipeModel>();

            CreateMap<SavedRecipes, SavedRecipeModel>();
            CreateMap<SavedRecipeModel, SavedRecipes>();

            CreateMap<RecipeComment, RecipeCommentModel>();
            CreateMap<CreateRecipeCommentModel, RecipeComment>();
        }
    }
}
