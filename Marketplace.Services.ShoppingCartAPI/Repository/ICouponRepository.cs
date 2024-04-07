using Marketplace.Services.ShoppingCartAPI.Models.Dto;

namespace Marketplace.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
