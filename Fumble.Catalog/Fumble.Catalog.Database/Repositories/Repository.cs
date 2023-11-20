using Fumble.Catalog.Database;

namespace Fumble.Catalog.Database.Repositories
{
    public abstract class Repository
    {
        public Repository(FumbleDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public FumbleDbContext DbContext { get; set; }
    }
}
