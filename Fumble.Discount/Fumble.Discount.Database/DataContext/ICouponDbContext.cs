using Fumble.Discount.Domain.Model;
using MongoDB.Driver;

namespace Fumble.Discount.Database.DataContext
{
    public interface ICouponDbContext
    {
        IMongoCollection<Coupon> CouponsCollection { get; }
    }
}