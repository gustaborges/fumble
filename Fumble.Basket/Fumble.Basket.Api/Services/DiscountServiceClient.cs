using Fumble.Discount.Grpc.Protos;
using Grpc.Net.Client;

namespace Fumble.Basket.Api.Services
{
    public class DiscountServiceClient : IDiscountServiceClient
    {
        private readonly Discount.Grpc.Protos.Discount.DiscountClient _client;

        public DiscountServiceClient(GrpcChannel grpcChannel)
        {
            _client = new Discount.Grpc.Protos.Discount.DiscountClient(grpcChannel);
        }

        public async Task<ApplyCouponResponse> GetDiscountAsync(string couponCode, string[] productsIds)
        {
            var applyCouponRequest = new ApplyCouponRequest();
            applyCouponRequest.CouponCode = couponCode;
            applyCouponRequest.ProductsIds.AddRange(productsIds);

            var response = await _client.ApplyAsync(applyCouponRequest);

            return response;
        }
    }
}
