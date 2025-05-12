using AutoMapper;
using Domain.Models.Order;
using Domain.Models.Products;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderItemPictureUrlResolver (IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                var URL = $"{configuration.GetSection("URLS")["BaseUrl"]}{source.Product.PictureUrl}";
                return URL;
            }
        }
    }
}
