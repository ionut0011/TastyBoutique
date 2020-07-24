using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Recipes.Extensions;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;

namespace TastyBoutique.Business.Recipes.Services.Implementations
{
    public sealed class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repository;
        private readonly IIngredientsRepo _ingredientsRepo;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepo repo, IMapper mapper, IIngredientsRepo irepo)
        {
            _repository = repo;
            _mapper = mapper;
            _ingredientsRepo = irepo;

        }

        public async Task<PaginatedList<RecipeModel>> Get(SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            var entities = await _repository.Get(spec);
            var count = await _repository.CountAsync();

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                entities.Count,
                count,
                _mapper.Map<IList<RecipeModel>>(entities));
        }
        public async Task<RecipeModel> Add(UpsertRecipeModel model)
        {
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);

            // a foreach for adding non-existing ingredients to the database
            foreach (var ingredient in model.IngredientsList)
                if(!_ingredientsRepo.Get(new SearchModel().ToSpecification<Ingredients>()).Result.Contains(ingredient))
                    await _ingredientsRepo.Add(_mapper.Map<Ingredients>(ingredient));
                
                
                
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
        public async Task<PaginatedList<FilterModel>> GetFiltersByRecipeId(Guid id)
        {
            var filters = _repository.GetFiltersByRecipeId(id);

            var filM = new List<Filters>();
            foreach (var fil in filters.Result)
                filM.Add(fil.Filter);

            return new PaginatedList<FilterModel>(
                1,
                filters.Result.Count,
                await _repository.CountAsync(),
                _mapper.Map<IList<FilterModel>>(filM));
        }
        public async Task<PaginatedList<IngredientModel>> GetIngredientsByRecipeId(Guid id)
        {
            var ingredients = _repository.GetIngredientsByRecipeId(id);

            IList<Ingredients> igM = new List<Ingredients>();
            foreach (var ing in ingredients.Result)
                igM.Add(ing.Ingredient);

            return new PaginatedList<IngredientModel>(
                1,
                ingredients.Result.Count,
                await _repository.CountAsync(),
                _mapper.Map<IList<IngredientModel>>(igM));
        }
    }
}
