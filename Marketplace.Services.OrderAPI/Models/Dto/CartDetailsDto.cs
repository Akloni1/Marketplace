﻿namespace Marketplace.Services.OrderAPI.Models.Dto
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public int Id { get; set; }
        public virtual ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
