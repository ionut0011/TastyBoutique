using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Recipes;
using TastyBoutique.Persistance.Repositories.Filters;
using TastyBoutique.Business.Models.Recipe;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Extensions;

namespace TastyBoutique.Business.Services.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly IIngredientsRepository _ingredientsRepo;
        private readonly IFiltersRepository _filtersRepo;
        private readonly IRecipeRepository _recipeRepo;
        private readonly IMapper _mapper;

        public SearchService(IIngredientsRepository ingredientsRepo, IFiltersRepository filtersRepo, IRecipeRepository recipeRepo, IMapper mapper)
        {
            _ingredientsRepo = ingredientsRepo;
            _filtersRepo = filtersRepo;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
        }

        //public async Task<IList<Ingredients>> MapIngredients(IList<string> ingredientsList)
        //{
        //    var result = new List<Ingredients>();

        //    foreach (var ingredient in ingredientsList)
        //    {
        //        result.Add(await _ingredientsRepo.GetByName(ingredient));
        //    }

        //    return result;
        //}

        public async Task<PaginatedList<TotalRecipeModel>> GetRecipiesByQuery(Guid idUser, IList<string> ingredientsList, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();

            List<Ingredients> ingredients = null;

            if (ingredientsList.Count != 0)
            {
                ingredients = new List<Ingredients>();
                foreach (var ingredient in ingredientsList)
                {
                    var temp = await _ingredientsRepo.GetByName(ingredient);
                    if (temp != null)
                        ingredients.Add(temp);
                    else
                        return new PaginatedList<TotalRecipeModel>(1, 0, 0, new List<TotalRecipeModel>());
                }
                   
            }

            var result = await _recipeRepo.GetRecipiesByQuery(idUser, ingredients, spec);
            //result.ToList().ForEach(c => c.Ingredients = c.RecipesIngredients.Select(x => x.Ingredient).ToList());
            //result.ToList().ForEach(c => c.Filters = c.RecipesFilters.Select(x => x.Filter).ToList());

            return new PaginatedList<TotalRecipeModel>(
                model.PageIndex,
                model.PageSize,
                result.Count,
                _mapper.Map<IList<TotalRecipeModel>>(result));
        }

        public  async Task<PaginatedList<TotalRecipeModel>> GetRecipiesByFilter(Guid idUser, string filter, SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Recipes>();
            var f = await _filtersRepo.GetByName(filter);
            var result = await _recipeRepo.GetRecipiesByFilter(idUser, f, spec);

            return new PaginatedList<TotalRecipeModel>(
                model.PageIndex,
                result.Count,
                result.Count,
                _mapper.Map<IList<TotalRecipeModel>>(result));
        }

    }
}
