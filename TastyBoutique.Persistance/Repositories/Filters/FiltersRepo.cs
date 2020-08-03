﻿using LinqBuilder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Repositories.Filters
{
    public sealed class FiltersRepo : Repository<Models.Filters>, IFiltersRepo 
    {
        public FiltersRepo(TastyBoutique_v2Context context) : base(context) { }

        public async Task<IList<Models.Filters>> Get(ISpecification<Models.Filters> spec)
            => await this.context.Filters.ExeSpec(spec).ToListAsync();

        public async Task<int> CountAsync()
            => await this.context.Filters.CountAsync();

        public async Task<Models.Filters> GetByName(String name)
            => await this.context.Filters.Where(i => i.Name.Equals(name)).FirstOrDefaultAsync();

    }
}
