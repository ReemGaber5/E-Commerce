using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepo<TEntity, TKey>(StoreDbContext context) : IGenericRepo<TEntity, TKey> where TEntity : ModelBase<TKey>
    {
        public void Add(TEntity entity)
            =>context.Set<TEntity>().Add(entity);
       

        public void Delete(TEntity entity)
         => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAll()
           =>await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetById(TKey id)
          =>await context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
          => context.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity, TKey> specifications)
        {
          return await SpecificationEvaluated.CreateQuery(context.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity> GetById(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluated.CreateQuery(context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec)
            => await SpecificationEvaluated.CreateQuery(context.Set<TEntity>(), spec).CountAsync();

    }
}

