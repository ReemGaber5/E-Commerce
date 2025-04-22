using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);

        Task<IEnumerable<TypeDTO>> GetAllTypes();

        Task<IEnumerable<BrandDTO>> GetAllBrands();

    }
}
