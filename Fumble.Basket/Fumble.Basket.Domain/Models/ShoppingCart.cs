
using Fumble.Basket.Api.Services.DiscountService.Dto;

namespace Fumble.Basket.Domain.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(string userId)
        {
            UserId = userId;
            RefreshLastUpdateTime();
        }

        public string UserId { get; }
        public IList<ShoppingCartItem> Items { get; } = new List<ShoppingCartItem>();
        public long LastUpdateUnixTime { get; private set;  }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }
        }

        public void ApplyDiscount(DiscountsInformation discounts)
        {
            throw new NotImplementedException();
        }

        public bool HasExpired(TimeSpan maxLifespan)
        {
            var lastUpdate = DateTimeOffset.FromUnixTimeMilliseconds(LastUpdateUnixTime).DateTime;

            return lastUpdate < DateTime.UtcNow.Add(maxLifespan.Negate());
        }

        public void RefreshLastUpdateTime()
        {
            LastUpdateUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
