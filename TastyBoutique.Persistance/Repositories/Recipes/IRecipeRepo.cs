﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Recipes
{
    public interface IRecipeRepo : IRepository<Models.Recipes>
    {
        Task<IList<Models.Recipes>> Get(Guid idUser, ISpecification<Models.Recipes> spec);

        Task<int> CountAsync();

        Task<IList<RecipesIngredients>> GetIngredientsByRecipeId(Guid id);
        Task<IList<RecipesFilters>> GetFiltersByRecipeId(Guid id);

        Task<IList<Models.Recipes>> GetRecipesUnpaginated();
        Task<List<RecipeComment>> GetCommentsReview(Guid idRecipe);
        Task<RecipeType> GetRecipeTypeById(Guid id);
        Task<Models.Recipes> GetByIdWithComments(Guid id);

        Task<List<Models.Recipes>> GetRecipiesByQuery(Guid idUser, IList<Models.Ingredients> ingredients, ISpecification<Models.Recipes> spec);

        Task<List<Models.Recipes>> GetRecipiesByFilter(Guid idUser, Filters filter, ISpecification<Models.Recipes> spec);
    }
}
