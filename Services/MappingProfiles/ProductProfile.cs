using AutoMapper;
using Domain.Models.Products;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<ProductResolver>());

            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
