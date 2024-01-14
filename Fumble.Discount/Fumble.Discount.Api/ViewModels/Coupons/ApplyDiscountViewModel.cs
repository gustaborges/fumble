using System.ComponentModel.DataAnnotations;

namespace Fumble.Discount.Api.ViewModels.Coupons
{
    public class ApplyDiscountViewModel
    {
        [Required]
        public required string CouponCode { get; set; }

        [Required]
        public required IList<string> ProductsIds { get; set; }
    }
}
