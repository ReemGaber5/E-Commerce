using Abstraction;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUOW uow,IMapper mapper,IBasketRepository basketRepository, UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(uow,mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationServices> _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, configuration,mapper));
        private readonly Lazy<IOrderService> _orderservice = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, uow));



        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;

        public IOrderService OrderService =>_orderservice.Value;
    }
}
