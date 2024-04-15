using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            return  basket is null ? NotFound(new ApiResponse(404 , $"Basket with id {id} not found")) :  Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _basketService.UpdateBasketAsync(basketDto);
            return basket is null ? NotFound(new ApiResponse(404, $"Basket with id {basketDto.Id} not found")) : Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await _basketService.DeleteBasketAsync(id));
        }
    }
}
