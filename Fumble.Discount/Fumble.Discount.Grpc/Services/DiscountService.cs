using Fumble.Discount.Domain.Model;
using Fumble.Discount.Domain.Repositories;
using Fumble.Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Fumble.Discount.Grpc.Services
{
    public class DiscountService(ICouponRepository couponRepository) : Protos.Discount.DiscountBase
    {
        private readonly ICouponRepository _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));

        public override async Task<ApplyCouponResponse> Apply(ApplyCouponRequest message, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetCouponAsync(message.CouponCode);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon {message.CouponCode} does not exist"));
            }

            if (!coupon.HasCampaignStarted || coupon.HasCampaignEnded)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Coupon's campaign has not started yet or has already ended"));
            }

            IList<string> eligibleProducts = message.ProductsIds.Where(coupon.IsProductEligible).ToList();

            ApplyCouponResponse response = new()
            {
                CouponCode = coupon.Code,
                Discount = coupon.Discount,
                DiscountType = (Protos.DiscountType)coupon.DiscountType,
                HasEligibleProducts = eligibleProducts.Count > 0,

            };
            response.EligibleProducts.Add(eligibleProducts);

            return response;
        }

        public override async Task<CreateCouponResponse> CreateCoupon(CreateCouponRequest message, ServerCallContext context)
        {
            var coupon = new Coupon()
            {
                Code = message.CouponCode,
                Description = message.Description,
                Discount = message.Discount,
                DiscountType = (Domain.Model.DiscountType)message.DiscountType,
                CampaignStartDate = message.CampaignStartDate.ToDateTime(),
                CampaignEndDate = message.CampaignEndDate?.ToDateTime(),
                ProductsIds = message.ProductsIds
            };

            try
            {
                await _couponRepository.CreateCouponAsync(coupon);
                
                return new CreateCouponResponse() { Id = coupon.Id.ToString() };
            }
            catch
            {
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while creating coupon"));
            }

        }

        public override async Task<GetCouponsResponse> GetCoupons(GetCouponsRequest request, ServerCallContext context)
        {

            var coupons = await _couponRepository.GetCouponsAsync();

            var couponsViewModels = coupons.Select(x => {
                var couponViewModel = new CouponViewModel()
                {
                    Description = x.Description,
                    Discount = x.Discount,
                    CouponCode = x.Code,
                    CampaignEndDate = ConvertToTimestamp(x.CampaignEndDate),
                    CampaignStartDate = ConvertToTimestamp(x.CampaignStartDate),
                    DiscountType = (Protos.DiscountType)x.DiscountType,
                };

                couponViewModel.ProductsIds.Add(x.ProductsIds);
                
                return couponViewModel;
            });


            var response = new GetCouponsResponse();
            response.Coupons.Add(couponsViewModels);

            return response;
        }

        private static Timestamp? ConvertToTimestamp(DateTime? campaignEndDate)
        {
            if (!campaignEndDate.HasValue)
                return null;

            return new Timestamp()
            {
                Seconds = new DateTimeOffset(campaignEndDate.Value).ToUnixTimeSeconds()
            };
        }
    }
}
