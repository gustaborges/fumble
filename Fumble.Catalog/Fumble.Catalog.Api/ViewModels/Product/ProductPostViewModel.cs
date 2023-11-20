using System.ComponentModel.DataAnnotations;

namespace Fumble.Catalog.Api.ViewModels.Product
{
    public class ProductPostViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IList<Guid> Categories { get; set; } = new List<Guid>();

    }
}
