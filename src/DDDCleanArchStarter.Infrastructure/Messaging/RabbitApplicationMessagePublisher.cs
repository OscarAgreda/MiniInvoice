using System;
using System.Text;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDCleanArchStarter.Infrastructure.Messaging
{
    public class RabbitApplicationMessagePublisher : IApplicationMessagePublisher
    {
        private readonly ILogger<RabbitApplicationMessagePublisher> _logger;
        private readonly DefaultObjectPool<IModel> _objectPool;
        public RabbitApplicationMessagePublisher(IPooledObjectPolicy<IModel> objectPolicy,
            ILogger<RabbitApplicationMessagePublisher> logger)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
            _logger = logger;
        }
        public void Publish(BaseDomainEvent baseDomainEvent)
        {
            Guard.Against.Null(baseDomainEvent, nameof(baseDomainEvent));
            var channel = _objectPool.Get();
            object message = baseDomainEvent;
            try
            {
                var exchangeName = MessagingConstants.Exchanges.DDDCLEANARCHSTARTER_RABBITMQMONGOWATCHER_EXCHANGE;
                channel.ExchangeDeclare(exchangeName, "direct", true, false, null);
                var messageString = JsonSerializer.Serialize(message);
                var sendBytes = Encoding.UTF8.GetBytes(messageString);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(
                    exchangeName,
                    "appointment-scheduled",
                    properties,
                    sendBytes);
                _logger.LogInformation($"Sending entity changed event: {messageString}");
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}