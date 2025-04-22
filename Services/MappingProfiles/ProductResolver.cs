using AutoMapper.Execution;
using Domain.Models.Products;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles
{
    public class ProductResolver (IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
              return string.Empty;
            else
            {
                var URL = $"{configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
                return URL;
            }

        }
    }
}
