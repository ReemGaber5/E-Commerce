﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepo<TEntity,TKey> where TEntity : ModelBase<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);

        Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity,TKey> specifications);
        Task<TEntity> GetById(ISpecifications<TEntity, TKey> specifications);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> CountAsync(ISpecifications<TEntity,TKey> spec); 


    }
}
