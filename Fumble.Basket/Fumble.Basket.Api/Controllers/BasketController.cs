using Fumble.Basket.Api.Services.DiscountService;
using Fumble.Basket.Api.Services.DiscountService.Dto;
using Fumble.Basket.Api.ViewModels;
using Fumble.Basket.Domain.Models;
using Fumble.Basket.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

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

        [HttpPost("{userId}")]
        [OutputCache(Duration = 900)]     
        public async Task<IActionResult> GetBasket([FromRoute] string userId, [FromQuery] string? couponCode, [FromServices]IDiscountServiceClient discountClient)
        {
            try
            {
                ShoppingCart basket = await _repository.GetBasketAsync(userId);
                
                if(basket.HasExpired(maxLifespan: TimeSpan.FromMinutes(15)))
                {
                    // TODO: Refresh basket: reverify stock, update prices, update basket
                }
                if (couponCode != null)
                {
                    var discounts = await discountClient.GetDiscountAsync(
                        couponCode, 
                        productsIds: basket.Items.Select(x => x.ProductId).ToArray()
                    );

                    basket.ApplyDiscount(discounts);
                }

                return Ok(ResponsePayload.Success(basket));
            }
            catch
            {
                return StatusCode(500, ResponsePayload.Error("FB_E50"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket, [FromServices] IDiscountServiceClient discountClient)
        {
            try
            {
                basket.RefreshLastUpdateTime();
                await _repository.UpdateBasketAsync(basket);

                return Ok();
            }
            catch
            {
                return StatusCode(500, ResponsePayload.Error("FB_E51"));
            }
        }


        private record GetBasketResponse(IList<string> BasketItems, DiscountsDto Discounts);
    }
}
