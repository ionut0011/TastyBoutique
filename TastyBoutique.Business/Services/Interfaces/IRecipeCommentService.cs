﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.RecipeComment;

namespace TastyBoutique.Business.Recipes.Services.Interfaces
{
    public interface IRecipeCommentService
    {
        Task<IEnumerable<RecipeCommentModel>> Get(Guid idRecipe);

        Task<RecipeCommentModel> Add(Guid IdRecipe, CreateRecipeCommentModel model);

        Task Delete(Guid IdRecipe, Guid commentId);

    }
}