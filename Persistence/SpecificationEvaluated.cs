using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluated
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity> input,ISpecifications<TEntity,Tkey> spec) where TEntity :ModelBase<Tkey>
        {
            var query = input;

            if(spec.Criteria!=null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy!=null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if (spec.IncludeExpressions != null && spec.IncludeExpressions.Count > 0)
            {
                //with list can nou use foreach but use aggregate fn 
                query = spec.IncludeExpressions.Aggregate(query, (currentquery, exp) => currentquery.Include(exp));
            }

            if(spec.IsPaginated==true)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }


            return query;

        }
    }
}
