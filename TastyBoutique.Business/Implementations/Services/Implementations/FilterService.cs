using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TastyBoutique.Business.Implementations.Models.Filter;
using TastyBoutique.Business.Implementations.Services.Interfaces;
using TastyBoutique.Business.Recipes.Extensions;
using TastyBoutique.Business.Recipes.Models.Ingredients;
using TastyBoutique.Business.Recipes.Models.Recipe;
using TastyBoutique.Persistance.Ingredients;
using TastyBoutique.Persistance.Models;
using TastyBoutique.Persistance.Repositories.Filters;

namespace TastyBoutique.Business.Implementations.Services.Implementations
{
    public sealed class FilterService : IFilterService
    {

        private readonly IFiltersRepo _repository;
        private readonly IMapper _mapper;

        public FilterService(IFiltersRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }
        public async Task<FilterModel> Add(CreateFilterModel model)
        {
            var filter = _mapper.Map<Persistance.Models.Filters>(model);
            await _repository.Add(filter);
            await _repository.SaveChanges();

            return _mapper.Map<FilterModel>(filter);
        }

        public async Task<PaginatedList<FilterModel>> Get(SearchModel model)
        {
            var spec = model.ToSpecification<Persistance.Models.Filters>();

            var entities = await _repository.Get(spec);
            var count = await _repository.CountAsync();

            return new PaginatedList<FilterModel>(
                model.PageIndex,
                entities.Count,
                count,
                _mapper.Map<IList<FilterModel>>(entities));
        }

        public async Task<FilterModel> GetId(PaginatedList<FilterModel> model, string name)
        {
            foreach (var res in model.Results)
            {
                if (res.Name.ToUpper().Equals(name.ToUpper()))
                    return _mapper.Map<FilterModel>(res);
            }

            return null;
        }

        public async Task<FilterModel> GetFilterByName(String name)
        {
            return _mapper.Map<FilterModel>(await _repository.GetByName(name));
        }

    }
}
