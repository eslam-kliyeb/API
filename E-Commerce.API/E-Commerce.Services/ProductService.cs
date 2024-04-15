using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Product;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Service;
using E_Commerce.Core.Specification;
using E_Commerce.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork , IMapper mapper) {
           _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();
            return (_mapper.Map<IEnumerable<BrandTypeDto>>(brands));
        }
        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return (_mapper.Map<IEnumerable<BrandTypeDto>>(types));
        }
        public async Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters specificationParameters)
        {
            var spec = new ProductSpecification(specificationParameters);
            var specCount = new ProductCountWithSpec(specificationParameters);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var count = await _unitOfWork.Repository<Product,int>().GetProductCountWithSpecAsync(specCount);
            var mappedProduct=_mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return new PaginatedResultDto<ProductToReturnDto>
            {
                Data = mappedProduct,
                PageIndex = specificationParameters.PageIndex,
                PageSize = specificationParameters.PageSize,
                TotalCount =count,
            };
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            return (_mapper.Map<ProductToReturnDto>(product));
        }
    }
}
