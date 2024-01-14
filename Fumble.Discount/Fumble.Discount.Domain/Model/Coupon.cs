namespace Fumble.Discount.Domain.Model
{
    public partial class Coupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CampaignStartDate { get; set; }
        public DateTime? CampaignEndDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public double Discount { get; set; }
        public IList<string> ProductsIds { get; set; } = new List<string>();
    }

    public partial class Coupon
    {
        public bool HasCampaignStarted { get => DateTime.UtcNow >= CampaignStartDate; }
        public bool HasCampaignEnded { get => DateTime.UtcNow > CampaignEndDate; }

        public bool IsProductEligible(string productId)
        {
            return ProductsIds.Contains(productId);
        }
    }
}
