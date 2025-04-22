using Abstraction;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.Products;
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
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var Repo = uow.GetRepo<Product, int>();
            var products = await Repo.GetAll();
            var MappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            return MappedProducts;
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
            var product = await uow.GetRepo<Product, int>().GetById(id);
            return mapper.Map<Product,ProductDTO>(product);

        }
    }
}
