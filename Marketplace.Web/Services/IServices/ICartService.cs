using Marketplace.Web.Models;

namespace Marketplace.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsnyc<T>(string userId, string token = "");
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = "");
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = "");
        Task<T> RemoveFromCartAsync<T>(int cartId, string token = "");
        Task<T> ApplyCoupon<T>(CartDto cartDto, string token = "");
        Task<T> RemoveCoupon<T>(string userId, string token = "");
        Task<T> Checkout<T>(CartHeaderDto cartHeader, string token = "");
    }
}
