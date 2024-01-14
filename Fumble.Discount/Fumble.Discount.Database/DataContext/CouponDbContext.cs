using Fumble.Discount.Domain.Model;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Fumble.Discount.Database.DataContext
{
    public class CouponDbContext : ICouponDbContext
    {
        public IMongoCollection<Coupon> CouponsCollection { get; }

        public CouponDbContext(CouponDatabaseConfigurations couponDbSettings)
        {
            ConfigureMapping();

            MongoClient mongoClient = new(couponDbSettings.ConnectionString);
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(couponDbSettings.DatabaseName);
            CouponsCollection = mongoDatabase.GetCollection<Coupon>(couponDbSettings.CouponsCollectionName);
        }

        private void ConfigureMapping()
        {
            var e = BsonClassMap.GetRegisteredClassMaps();
            BsonClassMap.RegisterClassMap(new BsonClassMap<Coupon>(classMap =>
            {
                classMap.MapIdProperty(c => c.Id).SetIdGenerator(new GuidGenerator());
                classMap.MapMember(c => c.CampaignStartDate);
                classMap.MapMember(c => c.CampaignEndDate);
                classMap.MapMember(c => c.Code);
                classMap.MapMember(c => c.Description);
                classMap.MapMember(c => c.Discount);
                classMap.MapMember(c => c.DiscountType);
                classMap.MapMember(c => c.ProductsIds);
            }));
        }
    }

}
