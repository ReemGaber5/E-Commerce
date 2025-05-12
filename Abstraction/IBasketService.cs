using Shared.DTOS.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO>GetBasketAsync(string key);
        Task<BasketDTO> CraeteorUpdateBasketAsync(BasketDTO basketDTO);
        Task<bool> DeleteBasketAsync(string key);


    }
}
