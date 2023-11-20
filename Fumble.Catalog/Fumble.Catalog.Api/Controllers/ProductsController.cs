using Fumble.Catalog.Api.ViewModels;
using Fumble.Catalog.Api.ViewModels.Category;
using Fumble.Catalog.Api.ViewModels.Product;
using Fumble.Catalog.Database;
using Fumble.Catalog.Database.Exceptions;
using Fumble.Catalog.Domain.Models;
using Fumble.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fumble.Controllers
{
    [Route("/api/v1/catalog/products")]
    public class ProductsController : Controller
    {
        private CatalogUnitOfWork _unitOfWork;

        public ProductsController(
            [FromServices] CatalogUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponsePayload<ProductGetViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts([FromQuery] int take=15, [FromQuery] int skip=0)
        {
            try
            {
                IEnumerable<Product> products = await _unitOfWork.ProductRepository.GetProductsAsync(take, skip, includeCategories: true);

                var result = products.Select(p => new ProductGetViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreatedAt = p.CreatedAt,
                    Price = p.Price,
                    Categories = p.Categories.Select(c => c.Id).ToList()
                });


                return Ok(ResponsePayload.Success(result));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCP_E50"));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponsePayload<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostProduct([FromBody] ProductPostViewModel model)
        {
            try
            {
                if (!await _unitOfWork.CategoryRepository.CanFindCategoriesAsync(model.Categories))
                {
                    return BadRequest(ResponsePayload.Error("FCP_E40", new() { "One or more categories do not exist" }));
                }

                Product product = new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    CreatedAt = DateTime.UtcNow,
                    Categories = model.Categories.Select(id => new Category { Id = id }).ToList()
                };

                await _unitOfWork.ProductRepository.CreateProductAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return Created(string.Empty, ResponsePayload.Success(product.Id));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCP_E51"));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SuccessResponsePayload<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
                await _unitOfWork.ProductRepository.DeleteProductAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, ResponsePayload.Success($"Deleted product {id}"));
            }
            catch(EntityNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponsePayload.Error("FCP_E40", new() { ex.Message }));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FCP_E51"));
            }
        }
    }
}
