using Ecommerce.ShoppingCartAPI.Data.Entities;
using Ecommerce.ShoppingCartAPI.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Ecommerce.ShoppingCartAPI.RabbitMQ
{
    public class ProductReceiverService : BackgroundService
    {
        private IServiceProvider _sp;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;


        public ProductReceiverService(IServiceProvider sp)
        {
            _sp = sp;

            _factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = _factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "Products",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Task.Run(async () =>
                {
                    var chunks = message.Split("|");

                    var item = new Item();
                    var userName = chunks[0];

                    if (chunks.Length == 3)
                    {
                        item.ProductName = chunks[1];
                        item.Price = Convert.ToDouble(chunks[2]);
                        item.Count = 1;
                    }

                    using (var scope = _sp.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<IShoppingCartRepository>();
                        await db.AddItemToCartAsync(userName, item);
                    }
                });
            };

            _channel.BasicConsume(queue: "Products", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
