using Fumble.Discount.Domain.Model;

namespace Fumble.Discount.Domain.Repositories
{
    public interface ICouponRepository
    {
        Task CreateCouponAsync(Coupon coupon);
        Task<Coupon> GetCouponAsync(string code);
        Task<IList<Coupon>> GetCouponsAsync();
    }
}
