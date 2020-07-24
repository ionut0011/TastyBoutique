using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance
{
    public abstract class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly TastyBoutique_v2Context context;

        protected Repository(TastyBoutique_v2Context context)
        {
            this.context = context;
        }

        public async Task<T> GetById(Guid id)
            => await this.context.Set<T>().FindAsync(id);

        public async Task Add(T entity)
            => await this.context.Set<T>().AddAsync(entity);

        public async Task<T> GetId(string name)
            => await this.context.Set<T>().FindAsync(name);

        public void Update(T entity)
            => this.context.Set<T>().Update(entity);

        public void Delete(T entity)
            => this.context.Set<T>().Remove(entity);

        public Task SaveChanges()
            => this.context.SaveChangesAsync();
    }
}
