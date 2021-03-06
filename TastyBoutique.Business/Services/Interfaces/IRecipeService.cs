﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<TotalRecipeModel> Add(UpsertRecipeModel model);
        Task<TotalRecipeModel> GetById(Guid id);
        Task <IList<TotalRecipeModel>> Get(SearchModel model);
        Task Update(Guid id, UpsertRecipeModel model);
        Task<bool> Delete(Guid id);
        Task<PaginatedList<FilterModel>> GetFiltersByRecipeId(Guid id);

        Task<PaginatedList<IngredientModel>> GetIngredientsByRecipeId(Guid id);



    }
}
