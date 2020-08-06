using AutoMapper;
using LinqBuilder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TastyBoutique.Business.Models.RecipeComment;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class RecipeCommentService : IRecipeCommentService
    {
        private readonly IRecipeRepo _repository;
        private readonly IMapper _mapper;

        public RecipeCommentService(IRecipeRepo repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<RecipeCommentModel> Add(Guid idUser, Guid idRecipe, CreateRecipeCommentModel model)
        {
            var comment = _mapper.Map<RecipeComment>(model);
            var recipe = await _repository.GetById(idRecipe);
            recipe.AddComment(comment);
            _repository.Update(recipe);
            await _repository.SaveChanges();
            return _mapper.Map<RecipeCommentModel>(comment);
        }

       
        public async Task Delete(Guid IdRecipe, Guid commentId)
        {
            var recipe = await _repository.GetByIdWithComments(IdRecipe);
            recipe.RemoveComment(commentId);
            _repository.Update(recipe);
            await _repository.SaveChanges();
        }


        public async Task<IEnumerable<RecipeCommentModel>> Get(Guid idRecipe)
        {
            var recipe = await _repository.GetByIdWithComments(idRecipe);
            
            return _mapper.Map<IEnumerable<RecipeCommentModel>>(recipe.RecipeComment);
        }
 
    }
}
