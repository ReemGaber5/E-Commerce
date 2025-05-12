using AutoMapper;
using Domain.Models.Basket;
using Shared.DTOS;
using Shared.DTOS.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTTO>().ReverseMap();

        }
    }
}
