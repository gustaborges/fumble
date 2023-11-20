using Fumble.Basket.Api.ViewModels;
using Fumble.Basket.Domain.Models;
using Fumble.Basket.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fumble.Basket.Api.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
            try
            {
                ShoppingCart basket = await _repository.GetBasketAsync(userId);
            
                return Ok(basket);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FB_E50"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            try
            {
                await _repository.UpdateBasketAsync(basket);

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("FB_E51"));
            }
        }
    }
}
