using Marketplace.Web.Models;
using Marketplace.Web.Services.IServices;

namespace Marketplace.Web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
