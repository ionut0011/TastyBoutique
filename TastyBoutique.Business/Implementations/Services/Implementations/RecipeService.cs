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
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TastyBoutique.Persistance.Repositories.Filters;

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

        public async Task<PaginatedList<RecipeModel>> Get(SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            var entities = await _repository.Get(spec);
            var count = await _repository.CountAsync();
            var recipes = _mapper.Map<IList<RecipeModel>>(entities);

            foreach (var recipe in recipes)
                recipe.Type = _repository.GetRecipeTypeById(recipe.Id).Result.Type;

            return new PaginatedList<RecipeModel>(
                model.PageIndex,
                entities.Count,
                count,
                recipes);
        }

        public async Task<RecipeModel> Add(UpsertRecipeModel model)
        {
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);

            foreach (var x in model.IngredientsList)
                recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, _mapper.Map<Ingredients>(x)));
            

            foreach (var y in model.FiltersList)
                recipe.RecipesFilters.Add(new RecipesFilters(recipe, _mapper.Map<Filters>(y)));


            recipe.RecipeType = (model.Type == 1) ? new RecipeType(recipe, "Food") : new RecipeType(recipe, "Drink");
            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<RecipeModel>(recipe);
        }

        public async Task<RecipeModel> GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            var recipe = _mapper.Map<RecipeModel>(entity);
            recipe.Type = entity.Result.RecipeType.Type;
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

            var igM = new List<Ingredients>();
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
