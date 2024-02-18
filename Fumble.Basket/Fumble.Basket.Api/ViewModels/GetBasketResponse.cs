using Fumble.Basket.Api.Services.DiscountService.Dto;
using Fumble.Basket.Domain.Models;

namespace Fumble.Basket.Api.ViewModels
{
    public class GetBasketResponse
    {
        public ShoppingCart Basket { get; set; }
        public DiscountsInformation CouponDiscount { get; set; }

    }


}
