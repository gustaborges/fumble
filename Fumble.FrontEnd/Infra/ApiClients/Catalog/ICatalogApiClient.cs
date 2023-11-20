namespace Fumble.FrontEnd.Infra.ApiClients.Catalog
{
    public interface ICatalogApiClient
    {
        IEnumerable<Product> GetProducts(int take, int skip);
    }
}
