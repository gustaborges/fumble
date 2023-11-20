using Fumble.Basket.Domain.Models;

namespace Fumble.Basket.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userId);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket);
        Task DeleteBasketAsync(string userId);
    }
}