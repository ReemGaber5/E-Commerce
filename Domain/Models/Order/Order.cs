using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Order:ModelBase<Guid>
    {
        public Order(){}
        public Order(string userEmail, ICollection<OrderItem> items, decimal subtotal, OrderAddress address, DeliveryMethod deliveryMethod)
        {
            UserEmail = userEmail;
            Items = items;
            Subtotal = subtotal;
            Address = address;
            DeliveryMethod = deliveryMethod;
        }

        public string UserEmail { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal Subtotal { get; set; }
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public decimal GetTotal() => Subtotal + DeliveryMethod.Price;



    }
}
