using AutoMapper;
using Marketplace.Services.CouponAPI.Models;
using Marketplace.Services.CouponAPI.Models.Dto;

namespace Marketplace.Services.CouponAPI.AutoMapper
{
    public class MarketplaceProfiles : Profile
    {
        public MarketplaceProfiles()
        {
            CreateMap<CouponDto, Coupon>().ReverseMap();
        }
    }
}
