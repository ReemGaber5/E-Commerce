using Abstraction;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUOW uow,IMapper mapper) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=>new ProductService(uow,mapper));
        public IProductService ProductService => _productService.Value;
    }
}
