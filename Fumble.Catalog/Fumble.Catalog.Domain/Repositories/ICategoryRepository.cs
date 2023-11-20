using Fumble.Catalog.Domain.Models;

namespace Fumble.Catalog.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category category);
        Task<bool> CanFindCategoriesAsync(IList<Guid> ids);
        Task<IList<Category>> GetCategoriesAsync(IList<Guid> ids, bool includePosts=false);
        Task<IList<Category>> GetCategoriesAsync(int take, int skip, bool includePosts=false);
        Task<Category> GetCategoryAsync(Guid id, bool includePosts=false);
    }
}
