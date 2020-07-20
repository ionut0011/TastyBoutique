using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TastyBoutique.Business.Recipes.Models.Recipe;

namespace TastyBoutique.Business.Recipes
{
    public class RecipesMapping : Profile
    {
        public RecipesMapping()
        {
            CreateMap<UpsertRecipeModel, Persistance.Models.Recipes>();
            CreateMap<Persistance.Models.Recipes, RecipeModel>();
        }
    }
}
