using Fumble.Catalog.Domain.Models;

namespace Fumble.Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<IList<Product>> GetProductsAsync(int take, int skip, bool includeCategories=false);
    }
}
