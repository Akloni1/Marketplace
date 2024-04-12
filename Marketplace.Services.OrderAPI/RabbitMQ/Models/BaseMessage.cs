namespace Marketplace.Services.OrderAPI.RabbitMQ.Models
{
    public class BaseMessage
    {
        public int? Id { get; set; }
        public DateTime? MessageCreated { get; set; }
    }
}
