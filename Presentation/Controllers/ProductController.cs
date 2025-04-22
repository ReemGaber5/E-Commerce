using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController (IServiceManger serviceManger):ControllerBase
    {
        //getallproducts
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await serviceManger.ProductService.GetAll();

            //ok Function Return as json file 
            return Ok(products);

        }


        //getallbrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var brands = await serviceManger.ProductService.GetAllBrands();
            return Ok(brands);

        }

        //getalltypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await serviceManger.ProductService.GetAllTypes();
            return Ok(Types);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await serviceManger.ProductService.GetById(id);
            return Ok(product);

        }

    }
}
