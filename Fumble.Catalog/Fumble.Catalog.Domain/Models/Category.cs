namespace Fumble.Catalog.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IList<Product>? Products { get; set; }
    }
}
