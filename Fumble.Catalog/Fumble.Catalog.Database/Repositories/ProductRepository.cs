using Fumble.Catalog.Database.Exceptions;
using Fumble.Catalog.Domain.Models;
using Fumble.Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fumble.Catalog.Database.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(FumbleDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateProductAsync(Product product)
        {
            foreach (var c in product.Categories)
            {
                DbContext.Entry(c).State = EntityState.Unchanged;
            }

            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            Product product = await GetTrackableProductAsync(id);
            DbContext.Products.Remove(product);
            await DbContext.SaveChangesAsync();
        }

        private async Task<Product> GetTrackableProductAsync(Guid id)
        {
            Product? product = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
            if(product == null)
            {
                throw new EntityNotFoundException(nameof(Product), id);
            }

            return product;
        }

        public async Task<IList<Product>> GetProductsAsync(int take, int skip, bool includeCategories = false)
        {
            var query = DbContext.Products.AsNoTracking()
                .Skip(skip)
                .Take(take);

            if(includeCategories)
            {
                query = query.Include(x => x.Categories);
            }

            return await query.ToListAsync();
        }
    }
}
