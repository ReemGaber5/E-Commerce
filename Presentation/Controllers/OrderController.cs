using Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController(IServiceManger serviceManger) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);

            var order = await serviceManger.OrderService.CreateOrder(orderDTO, email);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DelieveryMethodDTO>>> GetMethod()
        {
            var method=await serviceManger.OrderService.GetDelieveryMethod();
            return Ok(method);

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllUserOrders()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var Oeders = await serviceManger.OrderService.GetAllOrders(Email);
            return Ok(Oeders);

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid Id)
        {
            var order=await serviceManger.OrderService.GetOrderbyId(Id);
            return Ok(order);

        }
        

    }
}
 