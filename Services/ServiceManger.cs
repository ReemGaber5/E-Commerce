using Abstraction;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUOW uow,IMapper mapper,IBasketRepository basketRepository, UserManager<ApplicationUser> userManager) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(uow,mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationServices> _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager));


        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;
    }
}
