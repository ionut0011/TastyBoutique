using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Recipes.Services.Implementations
{
    public sealed class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<RecipeModel> Add(UpsertRecipeModel model)
        {
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);
            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<RecipeModel>(recipe);
        }

        public async Task<RecipeModel> GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            var recipe = _mapper.Map<RecipeModel>(entity);
            return recipe;
        }

        public async Task Update(Guid id, UpsertRecipeModel model)
        {
            var recipe = await _repository.GetById(id);
            recipe.Update(model.Name, model.Access, model.Notifications, model.Image, model.Link, model.Notifications);

            _repository.Update(recipe);
            await _repository.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            var recipe = await _repository.GetById(id);
            _repository.Delete(recipe);
            await _repository.SaveChanges();
        }
    }
}
