using AutoMapper;
using LinqBuilder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TastyBoutique.Business.Recipes.Models.RecipeComment;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Recipes.Services.Implementations
{
    public sealed class RecipeCommentService : IRecipeCommentService
    {
        private readonly IRecipeRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public RecipeCommentService(IRecipeRepo _repository,IMapper _mapper, IHttpContextAccessor _accessor)
        {
            this._repository = _repository;
            this._mapper = _mapper;
            this._accessor=_accessor;
        }
        public async Task<RecipeCommentModel> Add(Guid idRecipe, CreateRecipeCommentModel model)
        {
            var claim = _accessor.HttpContext.User.Claims;
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
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
            var recipe = await _repository.GetById(idRecipe);
            return _mapper.Map<IEnumerable<RecipeCommentModel>>(recipe.RecipeComment);
        }
 
    }
}
