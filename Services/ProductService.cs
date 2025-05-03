using Abstraction;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models.Products;
using Services.Secifications;
using Shared;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService (IUOW uow,IMapper mapper) : IProductService
    {
        public async Task<PaginationResulte<ProductDTO>> GetAll(ProductParams productParams)
        {
             var Repo = uow.GetRepo<Product, int>();
            var Spec=new ProductSpecification(productParams);    

            var products = await Repo.GetAll(Spec);

            var MappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            var CountedProducts=products.Count();
            var countspec = new ProductCountSpecification(productParams);
            var TotalCount = await Repo.CountAsync(countspec);

            return new PaginationResulte<ProductDTO>(productParams.PageIndex, CountedProducts,TotalCount, MappedProducts);
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrands()
        {
            var Repo = uow.GetRepo<ProductBrand,int>();
            var Brands=await Repo.GetAll();
            var MappedBrand=mapper.Map<IEnumerable< ProductBrand>,IEnumerable< BrandDTO>>(Brands);
            return MappedBrand;
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypes()
        {
            var Repo=uow.GetRepo<ProductType,int>();
            var Types=await Repo.GetAll();
            var MappedTypes=mapper.Map<IEnumerable<ProductType>,IEnumerable< TypeDTO>>(Types);
            return MappedTypes;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var spec=new ProductSpecification(id);
            var product = await uow.GetRepo<Product, int>().GetById(spec);

            if (product == null)
                throw new ProductNotFoundException(id);

            return mapper.Map<Product,ProductDTO>(product);

        }
    }
}
