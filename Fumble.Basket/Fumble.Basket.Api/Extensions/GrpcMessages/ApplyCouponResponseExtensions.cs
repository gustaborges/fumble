using Fumble.Basket.Domain.Models;
using Fumble.Discount.Grpc.Protos;

namespace Fumble.Basket.Api.Extensions.GrpcMessages
{
    public static class ApplyCouponResponseExtensions
    {
        public static CouponDiscount ToCouponDiscount(this ApplyCouponResponse dto)
        {
            return new CouponDiscount
            {
                Discount = (decimal)dto.Discount,
                DiscountType = (Domain.Models.DiscountType) dto.DiscountType,
                CouponCode = dto.CouponCode,
                EligibleProducts = dto.EligibleProducts.ToArray()
            };
        }
    }
}
