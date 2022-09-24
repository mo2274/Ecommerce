using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Ecommerce.ProductsAPI.RabbitMQ
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("Products", exclusive: false, autoDelete: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "Products", body: body);
        }
    }
}
