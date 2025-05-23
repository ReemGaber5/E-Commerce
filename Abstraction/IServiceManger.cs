﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IOrderService OrderService { get; }
    }
}
