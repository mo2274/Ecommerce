using Ecommerce.ShoppingCartAPI.Data.Entities;
using Ecommerce.ShoppingCartAPI.Repositories;
using Ecommerce.ShoppingCartAPI.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
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
                    message = message.Substring(1, message.Length - 2);
                    message = message.Replace(@"\", string.Empty); ;
                    var itemModel = JsonConvert.DeserializeObject<ItemModel>(message);

                    Task.Run(async () =>
                    {

                        var item = new Item();
                        var userName = itemModel.UserName;
                        item.ProductName = itemModel.ProductName;
                        item.Price = itemModel.Price;
                        item.Count = itemModel.Count;

                        using (var scope = _sp.CreateScope())
                        {
                            var db = scope.ServiceProvider.GetRequiredService<IShoppingCartRepository>();
                            await db.AddItemToCartAsync(userName, item);
                        }
                    });
                };

                _channel.BasicConsume(queue: "Products", autoAck: true, consumer: consumer);
            }
            catch (Exception e)
            {

                throw;
            }


            return Task.CompletedTask;
        }
    }
}
