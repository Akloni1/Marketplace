using Marketplace.Services.CouponAPI.Models.Dto;
using Marketplace.Services.CouponAPI.Respository;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponApiController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        protected ResponseDto _response;

        public CouponApiController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            this._response = new ResponseDto();
        }

        [HttpGet("{code}")]
        public async Task<object> GetDiscountForCode(string code)
        {
            try
            {
                var coupon = await _couponRepository.GetCouponByCode(code);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
