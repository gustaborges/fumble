using Fumble.Basket.Domain.Models;
using Fumble.Discount.Grpc.Protos;
using Grpc.Net.Client;

namespace Fumble.Basket.Api.Services.DiscountService
{
    public class DiscountServiceClient : IDiscountServiceClient
    {
        private readonly GrpcChannel _channel;
        private readonly Discount.Grpc.Protos.Discount.DiscountClient _client;

        public DiscountServiceClient(string serviceUrl)
        {
            _channel = GrpcChannel.ForAddress(serviceUrl);
            _client = new Discount.Grpc.Protos.Discount.DiscountClient(_channel);
        }

        public async Task<DiscountsInformation> GetDiscountAsync(string couponCode, string[] productsIds)
        {
            var applyCouponRequest = new ApplyCouponRequest();
            applyCouponRequest.CouponCode = couponCode;
            applyCouponRequest.ProductsIds.AddRange(productsIds);

            var response = await _client.ApplyAsync(applyCouponRequest);

            return new DiscountsInformation(response);
        }
    }
}
