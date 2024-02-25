using Fumble.Basket.Domain.Models;

namespace Fumble.Basket.Api.ViewModels
{
    public record UpdateBasketRequest
    {
        public string UserId { get; set; } = string.Empty;
        public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
