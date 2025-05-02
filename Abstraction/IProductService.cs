using Shared;
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
        Task<PaginationResulte<ProductDTO>> GetAll(ProductParams productParams);
        Task<ProductDTO> GetById(int id);

        Task<IEnumerable<TypeDTO>> GetAllTypes();

        Task<IEnumerable<BrandDTO>> GetAllBrands();

    }
}
