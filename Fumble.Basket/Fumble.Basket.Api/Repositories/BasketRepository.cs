using Fumble.Basket.Domain.Models;
using Fumble.Basket.Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Fumble.Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasketAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userId)
        {
            string? basketJson = await _redisCache.GetStringAsync(userId);

            if(basketJson is null)
            {
                return ShoppingCart.Empty();
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basketJson)!;
        }

        public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
        {
            string basketJson = JsonSerializer.Serialize(basket);

            await _redisCache.SetStringAsync(basket.UserId, basketJson);

            return basket;
        }
    }
}
