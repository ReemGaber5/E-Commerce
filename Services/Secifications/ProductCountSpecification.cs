using Domain.Models.Products;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Secifications
{
    public class ProductCountSpecification:BaseSpecifications<Product,int>
    {

        public ProductCountSpecification(ProductParams productParams) : base(null)
        {

        }
    }
}
