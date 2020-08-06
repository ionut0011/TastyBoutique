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
        private readonly IRecipeRepo _repository;  
        private readonly IMapper _mapper;
        private readonly IIngredientsRepo _ingredients;
        private readonly IFiltersRepo _filters;
        private readonly ICollectionRepo _collections;
        private readonly IHttpContextAccessor _accessor;
        public RecipeService(IRecipeRepo repo, IMapper mapper, IFiltersRepo filter, IIngredientsRepo ingredient, ICollectionRepo collection, IHttpContextAccessor accessor)
        {
            _repository = repo;
            _mapper = mapper;
            _ingredients = ingredient;
            _filters = filter;
            _collections = collection;
            _accessor = accessor;
        }

        public async Task<IList<TotalRecipeModel>> Get(SearchModel model)
        {   /*
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            var entities = await _repository.Get(spec);
            var recipes = _mapper.Map<IList<TotalRecipeModel>>(entities);
            */
            IList<Persistance.Models.Recipes> entities = null;
            var tmp = _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser");

            if(tmp != null)
            {
                Guid idUser = Guid.Parse(tmp.Value);
                entities = await _repository.Get(idUser);
            }
            else
                entities = await _repository.GetAllPublic();

            var recipes = _mapper.Map<IList<TotalRecipeModel>>(entities);
            return recipes;
        }

        public async Task<TotalRecipeModel> Add(UpsertRecipeModel model)
        {
            model.IdUser = Guid.Parse(_accessor.HttpContext.User.Claims.First(c => c.Type == "IdUser").Value);
            var recipe = _mapper.Map<Persistance.Models.Recipes>(model);
            foreach (var ingredient in model.IngredientsList)
            {
                var ing = await _ingredients.GetByName(ingredient);
                if ( ing== null)
                    recipe.RecipesIngredients.Add(new RecipesIngredients(recipe, new Ingredients(ingredient)));
                else
                    recipe.RecipesIngredients.Add(new RecipesIngredients(recipe,ing));
            }
            
            var fil = await _filters.GetByName(model.Filter);
            if (fil == null) 
                recipe.RecipesFilters.Add(new RecipesFilters(recipe, new Filters(model.Filter)));
            else 
                recipe.RecipesFilters.Add(new RecipesFilters(recipe, fil));

            await _repository.Add(recipe);
            await _repository.SaveChanges();

            return _mapper.Map<TotalRecipeModel>(recipe);
        }

        public async Task<TotalRecipeModel> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            var recipe = _mapper.Map<TotalRecipeModel>(entity);
            return recipe;
        }

        public async Task Update(Guid id, UpsertRecipeModel model)
        {
            var recipe = await _repository.GetById(id);
            var ingredients = _mapper.Map<IList<Ingredients>>(GetIngredientsByRecipeId(recipe.Id).Result.Results);
            var filters = _mapper.Map<IList<Filters>>(GetFiltersByRecipeId(recipe.Id).Result.Results);
            
            
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
