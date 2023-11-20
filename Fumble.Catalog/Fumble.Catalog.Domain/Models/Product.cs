namespace Fumble.Catalog.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<Category> Categories { get; set; } = new List<Category>();
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
