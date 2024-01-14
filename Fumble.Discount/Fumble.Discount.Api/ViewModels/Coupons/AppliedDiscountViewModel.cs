using Fumble.Discount.Domain.Model;

namespace Fumble.Discount.Api.ViewModels.Coupons
{
    public class AppliedDiscountViewModel
    {
        public AppliedDiscountViewModel(Coupon coupon, IList<string> eligibleProducts)
        {
            this.EligibleProducts = eligibleProducts;
            this.AppliedCoupon = new AppliedCoupon(coupon.Code, coupon.DiscountType, coupon.Discount);
        }

        public bool HasEligibleProducts { get => EligibleProducts.Count > 0; }
        public IList<string> EligibleProducts { get; }
        public AppliedCoupon AppliedCoupon { get; }
    }

    public record AppliedCoupon(string Code, DiscountType DiscountType, double Discount);
}
