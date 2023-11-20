using Fumble.Catalog.Domain.Models;
using Fumble.Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fumble.Catalog.Database.Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(FumbleDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<Category>> GetCategoriesAsync(IList<Guid> ids, bool includeProducts=false)
        {
            var query = DbContext.Categories
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id));

            if (includeProducts )
            {
                query = query.Include(x => x.Products);
            }

            return await query.ToListAsync();

        }

        public async Task<IList<Category>> GetCategoriesAsync(int take, int skip, bool includeProducts=false)
        {
            IQueryable<Category> query = DbContext.Categories.AsNoTracking()
                .Skip(skip)
                .Take(take);

            if (includeProducts)
            {
                query = query.Include(x => x.Products);
            }

            return await query.ToListAsync();
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> CanFindCategoriesAsync(IList<Guid> ids)
        {
            bool canFindAllCategories = true;

            foreach (var id in ids)
            {
                canFindAllCategories = canFindAllCategories && await CanFindCategoryAsync(id);
            }

            return canFindAllCategories;
        }

        public async Task<bool> CanFindCategoryAsync(Guid id)
        {
            return await DbContext.Categories.AnyAsync(x => x.Id.Equals(id));
        }
    }
}
