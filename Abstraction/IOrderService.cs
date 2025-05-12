using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO,string Email);
        Task<IEnumerable<DelieveryMethodDTO>> GetDelieveryMethod();
        Task<OrderToReturnDTO> GetOrderbyId(Guid Id);
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrders(string  Email);


    }
}
