namespace Marketplace.Services.ShoppingCartAPI.RabbitMQ.Interfaces
{
    public interface IMessageRabbitMQProducer
    {
        void SendMessage<T>(T message);
    }
}
