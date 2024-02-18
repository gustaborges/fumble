namespace Fumble.Basket.Domain.Models
{

    public class DiscountsInformation
    {
        public string CouponCode { get; set; } = "";
        public List<string> EligibleProducts { get; set; } = [];
        public bool HasEligibleProducts => EligibleProducts.Count > 0;
        public DiscountType DiscountType { get; set; }
        public double Discount { get; set; }
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
