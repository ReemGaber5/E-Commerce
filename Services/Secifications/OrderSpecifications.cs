using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Secifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderSpecifications(string Email):base(e=>e.UserEmail==Email)
        {
            AddInclude(d => d.DeliveryMethod);
            AddInclude(d => d.Items);
            AddOrderByDesc(d => d.OrderDate);

        }

        public OrderSpecifications(Guid Id) : base(e => e.Id == Id)
        {
            AddInclude(d => d.DeliveryMethod);
            AddInclude(d => d.Items);
            AddOrderByDesc(d => d.OrderDate);

        }
    }
}
