using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Service;
using E_Commerce.Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Cash(60)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationParameters specificationParameters)
        {
            return Ok(await _productService.GetAllProductsAsync(specificationParameters));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            return  product is not null ? Ok(product) : NotFound(new ApiResponse(400 , $"Product With Id {id} Not Found"));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetBrands()
        {
            return Ok(await _productService.GetAllBrandsAsync());
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetTypes()
        {
            return Ok(await _productService.GetAllTypesAsync());
        }
    }
}
