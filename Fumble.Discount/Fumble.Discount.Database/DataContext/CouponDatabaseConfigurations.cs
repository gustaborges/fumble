namespace Fumble.Discount.Database.DataContext
{
    public record CouponDatabaseConfigurations
    (
        string ConnectionString,
        string DatabaseName,
        string CouponsCollectionName
    );
}
