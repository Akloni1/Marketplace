using Marketplace.Services.ShoppingCartAPI.Models.Dto;

namespace Marketplace.Services.ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        public Task<CouponDto> GetCoupon(string couponName)
        {
            throw new NotImplementedException();
        }
    }
}
