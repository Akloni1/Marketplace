using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using System;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using Marketplace.Services.OrderAPI.Models.Dto;
using Newtonsoft.Json;
using Marketplace.Services.OrderAPI.Models;
using Marketplace.Services.OrderAPI.Repository;

namespace Marketplace.Services.OrderAPI.RabbitMQ.Consumer
{
    public class RabbitMqListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqListener(IServiceProvider serviceProvider)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orders", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Debug.WriteLine($"Получено сообщение: {content}");
                var checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(content) ?? throw new Exception();
                OrderHeader orderHeader = new()
                {
                    UserId = checkoutHeaderDto.UserId,
                    FirstName = checkoutHeaderDto.FirstName,
                    LastName = checkoutHeaderDto.LastName,
                    OrderDetails = new List<OrderDetails>(),
                    CardNumber = checkoutHeaderDto.CardNumber,
                    CouponCode = checkoutHeaderDto.CouponCode,
                    CVV = checkoutHeaderDto.CVV,
                    DiscountTotal = checkoutHeaderDto.DiscountTotal,
                    Email = checkoutHeaderDto.Email,
                    ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                    OrderTime = DateTime.Now,
                    OrderTotal = checkoutHeaderDto.OrderTotal,
                    PaymentStatus = false,
                    Phone = checkoutHeaderDto.Phone,
                    PickupDateTime = checkoutHeaderDto.PickupDateTime
                };
                foreach (var detailList in checkoutHeaderDto.CartDetails)
                {
                    OrderDetails orderDetails = new()
                    {
                        Id = detailList.Id,
                        ProductName = detailList.Product.Name,
                        Price = detailList.Product.Price,
                        Count = detailList.Count
                    };
                    orderHeader.CartTotalItems += detailList.Count;
                    orderHeader.OrderDetails.Add(orderDetails);
                }

                using var scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                // Вызываем нужный метод контроллера
                await service.AddOrder(orderHeader);
                // Подтверждаем получение сообщения
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("orders", false, consumer);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
