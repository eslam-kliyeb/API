using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase(); 
        }
        public async Task<bool> DeleteCustomerBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket basket)
        {
            var serializerBasket= JsonSerializer.Serialize(basket);
            var res = await _database.StringSetAsync(basket.Id, serializerBasket,TimeSpan.FromDays(7));
            return (res  ? await GetCustomerBasketAsync(basket.Id) : null)!; 
        }
    }
}
