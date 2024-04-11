using Marketplace.Services.CouponAPI.Models.Dto;

namespace Marketplace.Services.CouponAPI.Respository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
