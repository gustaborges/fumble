namespace Fumble.Catalog.Api.ViewModels.Category
{
    public class CategoryGetViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IList<Guid>?  Products { get; set; }
    }
}