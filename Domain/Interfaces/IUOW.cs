using Domain.Models;
using Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUOW
    {
        IGenericRepo<TEntity,Tkey> GetRepo<TEntity, Tkey> ()where TEntity:ModelBase<Tkey>;
        Task<int> SaveChangesAsync();
    }
}
