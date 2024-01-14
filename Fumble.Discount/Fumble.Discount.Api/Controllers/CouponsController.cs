using Fumble.Discount.Api.ViewModels;
using Fumble.Discount.Api.ViewModels.Coupons;
using Fumble.Discount.Domain.Model;
using Fumble.Discount.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Fumble.Discount.Api.Controllers
{
    [Route("/api/v1/coupons")]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponsController([FromServices] ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        [HttpPost("apply")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AppliedDiscountViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ApplyCoupon([FromBody] ApplyDiscountViewModel viewModel)
        {
            var coupon = await _couponRepository.GetCouponAsync(viewModel.CouponCode);

            if (coupon is null)
            {
                return NotFound(ResponsePayload.Error("EDC_40", new() {$"Coupon {viewModel.CouponCode} does not exist"}));
            }

            if(coupon.HasCampaignStarted == false)
            {
                return BadRequest(ResponsePayload.Error("EDC_41", new() { $"Coupon's campaign has not started yet" }));
            }

            if (coupon.HasCampaignEnded)
            {
                return BadRequest(ResponsePayload.Error("EDC_42", new() { $"Coupon's campaign has already ended" }));
            }

            IList<string> eligibleProducts = viewModel.ProductsIds.Where(coupon.IsProductEligible).ToList();

            return Ok(ResponsePayload.Success(new AppliedDiscountViewModel(coupon, eligibleProducts)));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponViewModel viewModel)
        {
            var coupon = new Coupon()
            {
                Code = viewModel.Code,
                Description = viewModel.Description,
                Discount = viewModel.Discount,
                DiscountType = viewModel.DiscountType,
                CampaignEndDate = viewModel.CampaignEndDate,
                CampaignStartDate = viewModel.CampaignStartDate,
                ProductsIds = viewModel.ProductsIds
            };

            try
            {
                await _couponRepository.CreateCouponAsync(coupon);

                return Ok(ResponsePayload.Success(coupon.Id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponsePayload.Error("EDC_50"));
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoupons()
        {
            var coupons = await _couponRepository.GetCouponsAsync();

            return Ok(ResponsePayload.Success(coupons));
        }
    }
}
