using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public int DeliveyMethodId { get; set; }
        public AddressDTO ShipToAddress { get; set; }
    }
}
