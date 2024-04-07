using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int? CartDetailsId { get; set; }
        public int? CartHeaderId { get; set; }

        public virtual CartHeaderDto? CartHeader { get; set; }
        public int Id { get; set; }
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
