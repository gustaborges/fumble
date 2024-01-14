using Fumble.Discount.Domain.Repositories;
using Fumble.Discount.Grpc.Protos;
using Grpc.Core;

namespace Fumble.Discount.Grpc.Services
{
    public class DiscountService : Protos.Discount.DiscountBase
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        public override async Task<ApplyCouponResponse> Apply(ApplyCouponRequest model, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetCouponAsync(model.CouponCode);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon {model.CouponCode} does not exist"));
            }

            if (!coupon.HasCampaignStarted || coupon.HasCampaignEnded)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Coupon's campaign has not started yet or has already ended"));
            }

            IList<string> eligibleProducts = model.ProductsIds.Where(coupon.IsProductEligible).ToList();

            ApplyCouponResponse response = new()
            {
                CouponCode = coupon.Code,
                Discount = coupon.Discount,
                DiscountType = (DiscountType)coupon.DiscountType,
                HasEligibleProducts = eligibleProducts.Count > 0,

            };
            response.EligibleProducts.Add(eligibleProducts);

            return response;
        }
    }
}
