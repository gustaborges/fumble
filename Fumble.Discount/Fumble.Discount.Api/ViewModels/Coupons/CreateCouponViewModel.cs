using Fumble.Discount.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Fumble.Discount.Api.ViewModels.Coupons
{
    public class CreateCouponViewModel
    {
        [Required]
        public required string Code { get; set; }
        public string? Description { get; set; }
        
        [Required]
        public DateTime CampaignStartDate { get; set; }
        public DateTime? CampaignEndDate { get; set; }

        [Required]
        [AllowedValues(DiscountType.Percentage, DiscountType.FixedAmount)]
        public DiscountType DiscountType { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public IList<string> ProductsIds { get; set; } = new List<string>();
    }
}
