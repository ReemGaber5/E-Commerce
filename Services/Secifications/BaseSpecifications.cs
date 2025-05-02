using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Secifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : ModelBase<Tkey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;


        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();

        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }

        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc { get;  private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression) => OrderBy = OrderByExpression;

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression) => OrderByDesc = OrderByDescExpression;



        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; set; }

        protected void ApplyPagination(int pageSize,int pageIndex)
        {
            Take = pageSize;
            Skip =( pageIndex-1)*pageSize;
            IsPaginated = true;
        }
        #endregion
    }
}
