using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IRecipeRepository _repository;  
        private readonly IMapper _mapper;
        private readonly IIngredientsRepository _ingredients;
        private readonly IFiltersRepository _filters;
        private readonly ICollectionRepository _collections;
        private readonly IHttpContextAccessor _accessor;
        public RecipeService(IRecipeRepository repo, IMapper mapper, IFiltersRepository filter, IIngredientsRepository ingredient, ICollectionRepository collection, IHttpContextAccessor accessor)
        {
            _repository = repo;
            _mapper = mapper;
            _ingredients = ingredient;
            _filters = filter;
            _collections = collection;
            _accessor = accessor;
        }

        public async Task<IList<TotalRecipeModel>> Get(SearchModel model)
        {   
            IList<Persistance.Models.Recipes> entities = null;
            var tmp = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser");

            if(tmp != null)
            {
                Guid idUser = Guid.Parse(tmp.Value);
                entities = await _repository.Get(idUser);
                //entities.ToList().ForEach(c => c.Ingredients = c.RecipesIngredients.Select(x => x.Ingredient).ToList());
                //entities.ToList().ForEach(c => c.Filters = c.RecipesFilters.Select(x => x.Filter).ToList());
            }
            else
                entities = await _repository.GetAllPublic();
          
            //var test = _mapper.Map<IList<FilterModel>>(entities[0].RecipesFilters);
            var recipes = _mapper.Map<IList<TotalRecipeModel>>(entities);
            return recipes;
        }

        public async Task<TotalRecipeModel> Add(UpsertRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);
            model.Ingredients = model.Ingredients.Distinct().ToList();
            foreach (var ingredient in model.Ingredients)
            {
                var ing = await _ingredients.GetByName(ingredient);
                if ( ing== null)
                    recipe.Ingredients.Add(new RecipesIngredients(recipe, new Ingredients(ingredient)));
                else
                    recipe.Ingredients.Add(new RecipesIngredients(recipe,ing));
            }
            
            var fil = await _filters.GetByName(model.Filter);
            if (fil == null) 
                recipe.Filters.Add(new RecipesFilters(recipe, new Filters(model.Filter)));
            else 
                recipe.Filters.Add(new RecipesFilters(recipe, fil));
            
            //recipe.Ingredients = recipe.RecipesIngredients.Select(x => x.Ingredient).ToList();
            //recipe.Filters = recipe.RecipesFilters.Select(x => x.Filter).ToList();

            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<TotalRecipeModel>(recipe);
        }

        public async Task<TotalRecipeModel> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            //entity.Ingredients = entity.RecipesIngredients.Select(x => x.Ingredient).ToList();
            //entity.Filters = entity.RecipesFilters.Select(x => x.Filter).ToList();
            var recipe = _mapper.Map<TotalRecipeModel>(entity);
            return recipe;
        }

        public async Task Update(Guid id, UpsertRecipeModel model)
        {
            var recipe = await _repository.GetById(id);
            recipe.Ingredients.Clear();
            recipe.Filters.Clear();

            recipe.Name = model.Name;
            recipe.Access = model.Access;
            recipe.Description = model.Description;
            
            if (model.Image != null)
                recipe.Image = model.Image;
            
            recipe.Type = model.Type;
            model.Ingredients = model.Ingredients.Distinct().ToList();
            foreach (var ingredient in model.Ingredients)
            {
                var ing = await _ingredients.GetByName(ingredient);
                if (ing == null)
                    recipe.Ingredients.Add(new RecipesIngredients(recipe, new Ingredients(ingredient)));
                else
                    recipe.Ingredients.Add(new RecipesIngredients(recipe, ing));
            }

            var fil = await _filters.GetByName(model.Filter);
            if (fil == null)
                recipe.Filters.Add(new RecipesFilters(recipe, new Filters(model.Filter)));
            else
                recipe.Filters.Add(new RecipesFilters(recipe, fil));

            await _collections.SetAllByIdRecipe(recipe.Id);
            
            _repository.Update(recipe);
            await _repository.SaveChanges();
        }

        public async Task<bool> Delete(Guid id)
        {
            var userId = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var recipe = await _repository.GetById(id);
            if (recipe.IdUser == userId)
            {
                _repository.Delete(recipe);
                await _repository.SaveChanges();
                return true;
            }

            return false;
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
