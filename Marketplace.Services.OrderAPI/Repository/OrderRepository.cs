using AutoMapper;
using Marketplace.Services.OrderAPI.DbContexts;
using Marketplace.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.OrderAPI.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        protected IMapper _mapper;
        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Add(orderHeader);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            var orderHeaderFromDb = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.OrderHeaderId == orderHeaderId);
            if (orderHeaderFromDb != null)
            {
                orderHeaderFromDb.PaymentStatus = paid;
                await _db.SaveChangesAsync();
            }
        }
    }
}
