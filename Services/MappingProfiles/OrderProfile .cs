using AutoMapper;
using Domain.Models.Order;
using Shared.DTOS.IdentityDTOs;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();

            CreateMap<Order,OrderToReturnDTO>().
                ForMember(dest=>dest.DeliveryMethod,opt=>opt.MapFrom(src=>src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDTO>().
              ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)).
              ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DelieveryMethodDTO>();




        }
    }
}
