using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
namespace DDDCleanArchStarter.Infrastructure.Messaging
{
    public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly IConnection _connection;
        public RabbitModelPooledObjectPolicy(
            IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _connection = GetConnection(rabbitMqOptions.Value);
        }
        public IModel Create()
        {
            return _connection.CreateModel();
        }
        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                return true;
            }
            obj?.Dispose();
            return false;
        }
        private IConnection GetConnection(RabbitMqConfiguration settings)
        {
            var factory = new ConnectionFactory
            {
                HostName = settings.Hostname,
                UserName = settings.UserName,
                Password = settings.Password,
                Port = settings.Port,
                VirtualHost = settings.VirtualHost
            };
            return factory.CreateConnection();
        }
    }
}