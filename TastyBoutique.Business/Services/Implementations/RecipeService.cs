using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Models.Filter;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TastyBoutique.Persistance.Repositories.Filters;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repository;  
        private readonly IMapper _mapper;
        private readonly IIngredientsRepo _ingredients;
        private readonly IFiltersRepo _filters;
        private readonly ICollectionRepo _collections;
      
        public RecipeService(IRecipeRepo repo, IMapper mapper, IFiltersRepo filter, IIngredientsRepo ingredient, ICollectionRepo collection)
        {
            _repository = repo;
            _mapper = mapper;
            _ingredients = ingredient;
            _filters = filter;
            _collections = collection;
        }

        public async Task<IList<TotalRecipeModel>> Get(Guid idUser, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            var entities = await _repository.Get(idUser, spec);
            var count = await _repository.CountAsync();
            var recipes = _mapper.Map<IList<TotalRecipeModel>>(entities);

            foreach (var recipe in recipes)
            {
                recipe.Type = _repository.GetRecipeTypeById(recipe.Id).Result.Type;
                recipe.Ingredients = GetIngredientsByRecipeId(recipe.Id).Result.Results;
                recipe.Filters = GetFiltersByRecipeId(recipe.Id).Result.Results;
            }

            return recipes;
        }

        public async Task<RecipeModel> Add(UpsertRecipeModel model)
        {
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);
            foreach (var ingredient in model.IngredientsList)
            {
                var ing = await _ingredients.GetByName(ingredient);
                if ( ing== null)
                    recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients(ingredient)));
                else
                    recipe.RecipesIngredients.Add(new RecipesIngredients(recipe,ing));
                
            }

            foreach (var filter in model.FiltersList)
            {
                var fil = await _filters.GetByName(filter);
                if (fil == null)
                    recipe.RecipesFilters.Add(new RecipesFilters(recipe, new Filters(filter)));
                else
                    recipe.RecipesFilters.Add(new RecipesFilters(recipe, fil));

            }

            recipe.RecipeType = (model.Type == 1) ? new RecipeType(recipe, "Food") : new RecipeType(recipe, "Drink");
            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<RecipeModel>(recipe);
        }

        public async Task<RecipeModel> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            var recipe = _mapper.Map<RecipeModel>(entity);
            recipe.Ingredients = GetIngredientsByRecipeId(recipe.Id).Result.Results;
            recipe.FiltersList = GetFiltersByRecipeId(recipe.Id).Result.Results;
            return recipe;
        }

        public async Task Update(Guid id, UpsertRecipeModel model)
        {
            var recipe = await _repository.GetById(id);

            recipe.Update(model.Name, model.Access,model.Description, model.Image);

            await _collections.SetAllByIdRecipe(recipe.Id);
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
