using Abstraction;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models.Basket;
using Shared.DTOS.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
    {
        public async Task<BasketDTO> CraeteorUpdateBasketAsync(BasketDTO basketDTO)
        {
            var customerBasket= mapper.Map<BasketDTO,CustomerBasket>(basketDTO);
            var newBasket=await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (newBasket != null)
                return await GetBasketAsync(basketDTO.Id);
            else
                throw new Exception("Can Not Create or Update Basket Now!");
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
           return await basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDTO> GetBasketAsync(string key)
        {
            var Basket=await basketRepository.GetBasketAsync(key);
            if (Basket != null)
                return mapper.Map<CustomerBasket, BasketDTO>(Basket);
            else 
                throw new BasketNotFoundException(key);

        }
    }
}
