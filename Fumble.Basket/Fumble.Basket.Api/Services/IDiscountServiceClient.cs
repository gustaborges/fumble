using Fumble.Discount.Grpc.Protos;

namespace Fumble.Basket.Api.Services
{
    public interface IDiscountServiceClient
    {
        Task<ApplyCouponResponse> GetDiscountAsync(string couponCode, string[] productsIds);
    }
}