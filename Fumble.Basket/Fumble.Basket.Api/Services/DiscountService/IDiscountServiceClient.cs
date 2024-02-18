
using Fumble.Basket.Domain.Models;

namespace Fumble.Basket.Api.Services.DiscountService
{
    public interface IDiscountServiceClient
    {
        Task<DiscountsInformation> GetDiscountAsync(string couponCode, string[] productsIds);
    }
}