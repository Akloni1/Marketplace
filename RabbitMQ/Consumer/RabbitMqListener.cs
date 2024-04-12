using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using System;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace RabbitMQ.Consumer
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


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Debug.WriteLine($"Получено сообщение: {content}");
              //  var order = JsonSerializer.Deserialize<Order>(content) ?? throw new Exception();
              //  using var scope = _serviceProvider.CreateScope();
               // var service = scope.ServiceProvider.GetRequiredService<IBankAccountService>();
                // Вызываем нужный метод контроллера
             //   service.PayOrder(order);
                // Подтверждаем получение сообщения
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("orders", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
