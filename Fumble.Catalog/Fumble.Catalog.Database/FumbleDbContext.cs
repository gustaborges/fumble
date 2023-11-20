using Fumble.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Fumble.Catalog.Database
{
    public class FumbleDbContext : DbContext
    {
        public FumbleDbContext(DbContextOptions<FumbleDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(products =>
            {
                products.HasKey(x => x.Id);

                products.Property(x => x.Id)
                    .HasDefaultValueSql("gen_random_uuid()");

                products.Property(x => x.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                products.Property(x => x.Price)
                    .HasColumnType("decimal(12,2)")
                    .IsRequired();

                products.Property(x => x.CreatedAt)
                    .IsRequired();

                products.HasMany(x => x.Categories)
                    .WithMany(x => x.Products);
            });

            modelBuilder.Entity<Category>(categories =>
            {
                categories.HasKey(x => x.Id);

                categories.Property(x => x.Id)
                    .HasDefaultValueSql("gen_random_uuid()");

                categories.Property(x => x.Name)
                    .HasMaxLength(60)
                    .IsRequired();

                categories.HasIndex(x => x.Name, name: "UQ_Categories_Name")
                    .IsUnique();

                categories.Property(x => x.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
