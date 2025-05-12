using Abstraction;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.Products;
using Services.Secifications;
using Shared.DTOS.IdentityDTOs;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper, IBasketRepository Repo, IUOW uow) : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrder(OrderDTO orderDTO, string Email)
        {
            var orderAddress = mapper.Map<AddressDTO, OrderAddress>(orderDTO.ShipToAddress);

            var basket = await Repo.GetBasketAsync(orderDTO.BasketId) ?? throw new BasketNotFoundException(orderDTO.BasketId);

            List<OrderItem> orderItems = [];
            var productRep = uow.GetRepo<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await productRep.GetById(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.quantity,
                    Product = new ProductItemOrdered()
                    {
                        Id = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl,

                    }
                });

            }

            //delivery Method
            var Delievery = await uow.GetRepo<DeliveryMethod, int>().GetById(orderDTO.DeliveyMethodId) ?? throw new DelieveryMethodNotFoundException(orderDTO.DeliveyMethodId);

            var Subtotal = orderItems.Sum(I => I.Quantity * I.Price);

            var Order = new Order(Email, orderItems, Subtotal, orderAddress, Delievery);

            uow.GetRepo<Order, Guid>().Add(Order);
            await uow.SaveChangesAsync();
            return mapper.Map<Order, OrderToReturnDTO>(Order);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrders(string Email)
        {
           var Spec=new OrderSpecifications(Email);
            var orders=await uow.GetRepo<Order,Guid>().GetAll(Spec);   
            return mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDTO>>(orders);    
        }

        public async Task<IEnumerable<DelieveryMethodDTO>> GetDelieveryMethod()
        {
            var method = await uow.GetRepo<DeliveryMethod, int>().GetAll();
            return mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DelieveryMethodDTO>>(method);
        }

        public async Task<OrderToReturnDTO> GetOrderbyId(Guid Id)
        {
            var spec=new OrderSpecifications(Id);
            var order=await uow.GetRepo<Order,Guid>().GetById(spec);

            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTO>>(order);
        }
    }
}
