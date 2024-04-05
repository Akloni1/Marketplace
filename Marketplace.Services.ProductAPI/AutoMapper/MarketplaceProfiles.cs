using AutoMapper;
using Marketplace.Services.ProductAPI.Models.Dto;
using Marketplace.Services.ProductAPI.Models;

namespace Marketplace.Services.ProductAPI.AutoMapper
{
    public class MarketplaceProfiles : Profile
    {
        public MarketplaceProfiles()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
