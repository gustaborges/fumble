using Fumble.Catalog.Api.ViewModels;
using Fumble.Catalog.Api.ViewModels.Category;
using Fumble.Catalog.Database;
using Fumble.Catalog.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fumble.Controllers
{
    [Route("/api/v1/catalog/categories")]
    public class CategoriesController : ControllerBase
    {
        private CatalogUnitOfWork _unitOfWork;

        public CategoriesController([FromServices] CatalogUnitOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponsePayload<IEnumerable<CategoryGetViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories([FromQuery] int take = 15, [FromQuery] int skip = 0)
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(take, skip, includePosts: true);

                var result = categories.Select(x => new CategoryGetViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    Products = x.Products?.Select(p => p.Id).ToList()
                });

                return Ok(ResponsePayload.Success(result));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCC_E50"));
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(typeof(SuccessResponsePayload<CategoryGetViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetCategoryAsync(id, includePosts: true);

                var result = new CategoryGetViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedAt = category.CreatedAt,
                    Products = category.Products?.Select(p => p.Id).ToList()
                };

                return Ok(ResponsePayload.Success(result));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCC_E50"));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponsePayload<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCategory([FromBody] CategoryPostViewModel model)
        {
            try
            {
                Category category = new()
                {
                    Name = model.Name,
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.CategoryRepository.CreateCategoryAsync(category);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction("GetCategory", category.Id, ResponsePayload.Success(category.Id)); ;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCC_E51"));
            }
        }
    }
}
