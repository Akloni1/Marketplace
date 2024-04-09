using AutoMapper;
using Marketplace.Services.ShoppingCartAPI.Models.Dto;
using Marketplace.Services.ShoppingCartAPI.Models;

namespace Marketplace.Services.ShoppingCartAPI.AutoMapper
{
    public class MarketplaceProfiles : Profile
    {
        public MarketplaceProfiles()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap(); 
        }
    }
}
