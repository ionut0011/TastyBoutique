using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Business.Recipes.Extensions;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Business.Recipes.Services.Interfaces;
using TastyBoutique.Persistance;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Business.Recipes.Services.Implementations
{
    public sealed class IngredientService : IIngredientService
    {
        private readonly IIngredientsRepo _repository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientsRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<PaginatedList<IngredientModel>> Get(SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Ingredients>();

            var entities = await _repository.Get(spec);
            var count = await _repository.CountAsync();

            return new PaginatedList<IngredientModel>(
                model.PageIndex,
                entities.Count,
                count,
                _mapper.Map<IList<IngredientModel>>(entities));
        }
        public async Task<IngredientModel> Add(CreateIngredientModel model)
        {
            var ingredient = _mapper.Map<Persistance.Models.Ingredients>(model);
            await _repository.Add(ingredient);
            await _repository.SaveChanges();

            return _mapper.Map<IngredientModel>(ingredient);
        }

        public async Task<IngredientModel> GetId(PaginatedList<IngredientModel> model, string name)
        {
            foreach (var res in model.Results)
            {
                if (res.Name.ToUpper().Equals(name.ToUpper()))
                    return _mapper.Map<IngredientModel>(res);
            }

            return null;
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
