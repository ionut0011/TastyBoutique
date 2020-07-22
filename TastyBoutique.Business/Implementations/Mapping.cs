using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance;

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


        }
    }
}
