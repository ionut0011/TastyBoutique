﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqBuilder.Core;

namespace TastyBoutique.Persistance.Repositories.Filters
{
    public interface IFiltersRepository : IRepository<Models.Filters>
    {
        Task<IList<Models.Filters>> Get(ISpecification<Models.Filters> spec);

        Task<int> CountAsync();

        Task<Models.Filters> GetByName(String name);
    }
}
