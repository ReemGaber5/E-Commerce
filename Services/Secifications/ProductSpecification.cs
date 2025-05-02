using Domain.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Secifications
{
    public class ProductSpecification:BaseSpecifications<Product,int>
    {
        public ProductSpecification(ProductParams productParams) : base(p => (!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || p.TypeId == productParams.TypeId)
        && (string.IsNullOrEmpty(productParams.SearchValue) || p.Name.ToLower().Contains(productParams.SearchValue.ToLower()))) 
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (productParams.SortingOptions)
            { 
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                    default:
                    break;
            }

            ApplyPagination(productParams.PageSize, productParams.PageIndex);
        }

        public ProductSpecification(int Id):base(p=>p.Id==Id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

        }
    }
}
