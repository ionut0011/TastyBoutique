﻿using System;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;

namespace TastyBoutique.Business.Services.Interfaces
{
    public interface ICollectionService
    {
        Task Add(SavedRecipeModel model);
        Task Delete(Guid recipeId);
        Task Update(SavedRecipeModel model);
        public Task<PaginatedList<RecipeModel>> GetAllByIdUser(SearchModel model);
    }
}
