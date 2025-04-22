using Domain.Interfaces;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UOW(StoreDbContext context) : IUOW
    {
        private readonly Dictionary<string, object> _Repos=new Dictionary<string, object>();
        public IGenericRepo<TEntity, Tkey> GetRepo<TEntity, Tkey>() where TEntity : ModelBase<Tkey>
        {
           var typename=typeof(TEntity).Name;
           
           if(_Repos.ContainsKey(typename) )
                return (IGenericRepo<TEntity, Tkey>) _Repos[typename];

            var NewRepo = new GenericRepo<TEntity, Tkey>(context);
            _Repos.Add(typename, NewRepo);

            return NewRepo;


        }

        public async Task<int> SaveChangesAsync()
        {
           return await context.SaveChangesAsync();
        }
    }
}
