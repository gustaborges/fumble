namespace Fumble.Catalog.Api.ViewModels.Product
{
    internal class ProductGetViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<Guid> Categories { get; set; } = new List<Guid>();
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}