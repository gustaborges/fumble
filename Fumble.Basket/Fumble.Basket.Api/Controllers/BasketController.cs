using Fumble.Basket.Api.Extensions.GrpcMessages;
using Fumble.Basket.Api.Services;
using Fumble.Basket.Api.ViewModels;
using Fumble.Basket.Domain.Models;
using Fumble.Basket.Domain.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetBasket([FromRoute] string userId, [FromQuery] string? couponCode, [FromServices]IDiscountServiceClient discountClient)
        {
            try
            {
                ShoppingCart basket = await _repository.GetBasketAsync(userId);

                if (couponCode != null)
                {
                    var discountResponse = await discountClient.GetDiscountAsync
                    (
                        couponCode,
                        productsIds: basket.Items.Select(x => x.ProductId).ToArray()
                    );

                    basket.ApplyDiscount(discountResponse.ToCouponDiscount());
                }

                return Ok(ResponsePayload.Success(basket));
            }
            catch(RpcException ex)
            {
                return BadRequest(ResponsePayload.Error("FB_E40", [ex.Status.Detail]));
            }
            catch
            {
                return StatusCode(500, ResponsePayload.Error("FB_E51"));

            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponsePayload), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBasket([FromBody] UpdateBasketRequest basket, [FromServices] IDiscountServiceClient discountClient)
        {
            try
            {
                var model = new ShoppingCart(basket.UserId, basket.Items);
                await _repository.UpdateBasketAsync(model);

                return Ok();
            }
            catch
            {
                return StatusCode(500, ResponsePayload.Error("FB_E51"));
            }
        }
    }
}
