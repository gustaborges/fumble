using Fumble.Discount.Database.DataContext;
using Fumble.Discount.Domain.Model;
using Fumble.Discount.Domain.Repositories;
using MongoDB.Driver;

namespace Fumble.Discount.Database.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ICouponDbContext _dbContext;

        public CouponRepository(ICouponDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateCouponAsync(Coupon coupon)
        {
            await _dbContext.CouponsCollection.InsertOneAsync(coupon);
        }

        public async Task<Coupon> GetCouponAsync(string code)
        {
            var coupons = await _dbContext.CouponsCollection.FindAsync(coupon => coupon.Code == code);

            return await coupons.SingleOrDefaultAsync();
        }

        public async Task<IList<Coupon>> GetCouponsAsync()
        {
            var coupons = await _dbContext.CouponsCollection.FindAsync(_ => true);

            return await coupons.ToListAsync();
        }
    }
}
