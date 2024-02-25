using Fumble.Basket.Domain.Models;

namespace Fumble.Basket.Api.ViewModels
{
    public record GetBasketResponse(IList<ShoppingCartItem> Items , CouponDiscount? CouponDiscount);
}
