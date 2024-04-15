using E_Commerce.Core.Entities;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Service
{
    public interface IProductService
    {
        Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specificationParameters);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();

    }
}
