namespace Fumble.Basket.Domain.Models
{
    public class ShoppingCart
    {
        private ShoppingCart() : this(string.Empty, []) { }

        public ShoppingCart(string userId, IList<ShoppingCartItem> items)
        {
            UserId = userId;
            Items = items;
            RefreshLastUpdateTime();
        }

        public string UserId { get; init; }
        public IList<ShoppingCartItem> Items { get; init; }
        public long LastUpdateUnixTime { get; private set;  }
        public CouponDiscount? CouponDiscount { get; private set; }

        public decimal TotalPrice => CalculateTotalPrice(Items);

        public decimal TotalPriceWithDiscount
        {
            get
            {
                if (CouponDiscount == null || !CouponDiscount.HasEligibleProducts)
                    return CalculateTotalPrice(Items);

                if (CouponDiscount.DiscountType == DiscountType.PercentageOnTotal)
                    return CalculateTotalPrice(Items) * CouponDiscount.Discount;

                if (CouponDiscount.DiscountType == DiscountType.FixedAmountOnTotal)
                    return Math.Max(0, CalculateTotalPrice(Items) - CouponDiscount.Discount);

                if (CouponDiscount.DiscountType == DiscountType.PercentageOnEligibleProducts)
                    return CalculateTotalPrice(GetItemsInelegibleForDiscount()) + CalculateTotalPrice(GetItemsElegibleForDiscount()) * (1 - CouponDiscount.Discount);

                if (CouponDiscount.DiscountType == DiscountType.FixedAmountOnEligibleProducts)
                    return CalculateTotalPrice(GetItemsInelegibleForDiscount()) + (Math.Max(0, CalculateTotalPrice(GetItemsElegibleForDiscount()) - CouponDiscount.Discount));

                throw new Exception($"Discount type {CouponDiscount.DiscountType} not recognized");
            }
        }

        private List<ShoppingCartItem> GetItemsInelegibleForDiscount()
        {
            if (CouponDiscount is null)
                return [];

            return Items.Where(x => !CouponDiscount.EligibleProducts.Contains(x.ProductId)).ToList();
        }

        private List<ShoppingCartItem> GetItemsElegibleForDiscount()
        {
            if (CouponDiscount is null)
                return [];

            return Items.Where(x => CouponDiscount.EligibleProducts.Contains(x.ProductId)).ToList();
        }

        private decimal CalculateTotalPrice(IList<ShoppingCartItem> items)
        {
            decimal totalPrice = 0;

            foreach (var item in items)
            {
                totalPrice += item.Price * item.Quantity;
            }

            return totalPrice;
        }

        public void ApplyDiscount(CouponDiscount couponDiscount)
        {
            CouponDiscount = couponDiscount;
        }


        public void RefreshLastUpdateTime()
        {
            LastUpdateUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        public static ShoppingCart Empty()
        {
            return new ShoppingCart();
        }
    }
}
