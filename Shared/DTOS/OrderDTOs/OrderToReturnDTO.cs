using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class OrderToReturnDTO
    {
        public Guid ID { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } 
        public AddressDTO ShipToAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public ICollection<OrderItemDTO> Items { get; set; } = [];

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }  
    }
}
