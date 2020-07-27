using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TastyBoutique.Business.Collections.Models;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Recipes
{
    public class RecipesMapping : Profile
    {
        public RecipesMapping()
        {
            
            CreateMap<SavedRecipes, SavedRecipeModel>();
            CreateMap<SavedRecipeModel, SavedRecipes>();
        }
    }
}
