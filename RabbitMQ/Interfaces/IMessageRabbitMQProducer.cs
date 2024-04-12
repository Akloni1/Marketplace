using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Interfaces
{
    public interface IMessageRabbitMQProducer
    {
        void SendMessage<T>(T message);
    }
}
