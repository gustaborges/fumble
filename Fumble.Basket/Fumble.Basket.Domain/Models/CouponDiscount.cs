namespace Fumble.Basket.Domain.Models
{

    public class CouponDiscount
    {
        public string CouponCode { get; set; } = "";
        public string[] EligibleProducts { get; set; } = [];
        public bool HasEligibleProducts => EligibleProducts.Length > 0;
        public DiscountType DiscountType { get; set; }
        public decimal Discount { get; set; }
    }

    public enum DiscountType
    {
        Unspecified = 0,
        PercentageOnEligibleProducts = 1,
        PercentageOnTotal = 2,
        FixedAmountOnEligibleProducts = 3,
        FixedAmountOnTotal = 4
    }
}
