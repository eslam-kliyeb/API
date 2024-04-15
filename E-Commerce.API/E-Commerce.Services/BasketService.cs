using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;
        public BasketService(IBasketRepository repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            
            return await _repository.DeleteCustomerBasketAsync(id);
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket = await _repository.GetCustomerBasketAsync(id);
            return  (basket is null ? null :_mapper.Map<BasketDto>(basket))!;
        }

        public async Task<BasketDto> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var updateBasket = await _repository.UpdateCustomerBasketAsync(customerBasket);
            return (updateBasket is null ? null : _mapper.Map<BasketDto>(updateBasket))!;
        }
    }
}
