using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS.BasketDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasketController(IServiceManger serviceManger) : ControllerBase
    {
        //Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string key)
        {
            var basket =await serviceManger.BasketService.GetBasketAsync(key);
            return Ok(basket);
            
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateorUpdateBasket(BasketDTO basket)
        {
            var Basket=await serviceManger.BasketService.CraeteorUpdateBasketAsync(basket);
            return Ok(Basket);

        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result=await serviceManger.BasketService.DeleteBasketAsync(key); 
            return Ok(result);
            
        }
    }
}
