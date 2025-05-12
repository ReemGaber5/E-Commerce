using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public enum OrderStatus
    {
        pending=0,
        paymentRecieved=1,
        paymentDeclined= 2,

    }
}
