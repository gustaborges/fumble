using System.ComponentModel.DataAnnotations;

namespace Fumble.Catalog.Api.ViewModels.Category
{
    public class CategoryPostViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

    }
}