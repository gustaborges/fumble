using Fumble.Catalog.Database.Repositories;
using Fumble.Catalog.Domain.Repositories;

namespace Fumble.Catalog.Database
{
    public class CatalogUnitOfWork
    {
        private readonly FumbleDbContext _dbContext;
        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;

        public CatalogUnitOfWork(FumbleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepository ProductRepository
        {
            get => _productRepository ??= new ProductRepository(_dbContext);
        }

        public ICategoryRepository CategoryRepository
        {
            get => _categoryRepository ??= new CategoryRepository(_dbContext);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
