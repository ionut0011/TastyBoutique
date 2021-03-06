﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class RecipeCommentService : IRecipeCommentService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeCommentService(IRecipeRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<RecipeCommentModel> Add(Guid idUser, Guid idRecipe, CreateRecipeCommentModel model)
        {
            var comment = _mapper.Map<RecipeComment>(model);
            comment.IdUser = idUser;
            comment.IdRecipe = idRecipe;

            var recipe = await _repository.GetById(idRecipe);
            recipe.AddComment(comment);
            _repository.Update(recipe);

            await _repository.SaveChanges();
            return _mapper.Map<RecipeCommentModel>(comment);
        }


        public async Task<bool> Delete(Guid commentId, Guid userId)
        {
            var comment = await _repository.GetRecipeComment(commentId);
            if (comment != null && comment.IdUser == userId)
            {
                await _repository.DeleteComment(commentId);
                await _repository.SaveChanges();
                return true;
            }

            return false;
        }


        public async Task<IEnumerable<RecipeCommentModel>> Get(Guid idRecipe)
        {
            var recipe = await _repository.GetByIdWithComments(idRecipe);

            return _mapper.Map<IEnumerable<RecipeCommentModel>>(recipe.RecipeComment);
        }

    }
}