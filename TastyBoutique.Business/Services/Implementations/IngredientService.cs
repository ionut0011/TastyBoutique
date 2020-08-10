using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastyBoutique.Business.Extensions;
using TastyBoutique.Business.Models.Ingredients;
using TastyBoutique.Business.Models.Shared;
using TastyBoutique.Business.Services.Interfaces;
using TastyBoutique.Persistance.Ingredients;

namespace TastyBoutique.Business.Services.Implementations
{
    public sealed class IngredientService : IIngredientService
    {
        private readonly IIngredientsRepository _repository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientsRepository repo, IMapper mapper)
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

        //public async Task<IngredientModel> GetId(PaginatedList<IngredientModel> model, string name)
        //{
        //    foreach (var res in model.Results)
        //    {
        //        if (res.Name.ToUpper().Equals(name.ToUpper()))
        //            return _mapper.Map<IngredientModel>(res);
        //    }

        //    return null;
        //}

        //public async Task<IngredientModel> GetByName(String name)
        //{
        //    return _mapper.Map<IngredientModel>(await _repository.GetByName(name));
        //}

    }
}
