using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DDDCleanArchStarter.Api.Hubs;
using DDDCleanArchStarter.Infrastructure.Messaging;
using DDDInvoicingCleanL.SharedKernel;
namespace DDDCleanArchStarter.Api
{
    public class RabbitMQMongoService : BackgroundService
    {
        private readonly string _exchangeName = MessagingConstants.Exchanges.DDDCLEANARCHSTARTER_RABBITMQMONGOWATCHER_EXCHANGE;
        private readonly ILogger<RabbitMQMongoService> _logger;
        private readonly string _queuein = MessagingConstants.Queues.FDVCP_DDDCLEANARCHSTARTER_IN;
        private readonly IHubContext<DDDCleanArchStarterHub> _scheduleHub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IModel _channel;
        private IConnection _connection;
        public RabbitMQMongoService(
            IOptions<RabbitMqConfiguration> rabbitMqOptions,
            ILogger<RabbitMQMongoService> logger,
            IServiceScopeFactory serviceScopeFactory,
            IHubContext<DDDCleanArchStarterHub> scheduleHub)
        {
            var settings = rabbitMqOptions.Value;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _scheduleHub = scheduleHub;
            InitializeConnection(settings);
        }
        private void InitializeConnection(RabbitMqConfiguration settings)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = settings.Hostname,
                    UserName = settings.UserName,
                    Password = settings.Password,
                    VirtualHost = settings.VirtualHost,
                    Port = settings.Port
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(_exchangeName, "direct",
                    true,
                    false,
                    null);
                _channel.QueueDeclare(_queuein,
                    true,
                    false,
                    false,
                    null);
                var routingKey = "appointment-confirmation";
                _channel.QueueBind(_queuein, _exchangeName, routingKey);
                _logger.LogInformation($"*** Listening for messages on {_exchangeName}-{routingKey}...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, settings.ToString());
                Thread.Sleep(5000);
                throw;
            }
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageReceived;
            _channel.BasicConsume(_queuein,
                true,
                consumer);
            return Task.CompletedTask;
        }
        private async void OnMessageReceived(object model, BasicDeliverEventArgs args)
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(" [x] Received {0}", message);
            await HandleMessage(message);
        }
        private async Task HandleMessage(string message)
        {
            _logger.LogInformation($"Handling Message: {message}");
            using var doc = JsonDocument.Parse(message);
            var root = doc.RootElement;
            var eventType = root.GetProperty("EventType");
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            if (eventType.GetString() == nameof(InvoiceConfirmLinkClickedIntegrationEvent))
            {
                var invoiceId = root.GetProperty("InvoiceId").GetGuid();
                var dateTime = root.GetProperty("DateTimeEventOccurred").GetDateTime();
                var appEvent = new InvoiceConfirmLinkClickedIntegrationEvent(dateTime)
                {
                    InvoiceId = invoiceId
                };
                await mediator.Publish(appEvent);
            }
            else
            {
                throw new Exception($"Unknown message type: {eventType.GetString()}");
            }
        }
        public override void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            _connection.Dispose();
            base.Dispose();
        }
    }
    public class InvoiceConfirmLinkClickedIntegrationEvent : BaseIntegrationEvent
    {
        public InvoiceConfirmLinkClickedIntegrationEvent() : this(DateTime.UtcNow)
        {
        }
        public InvoiceConfirmLinkClickedIntegrationEvent(DateTime dateOccurred)
        {
            OccurredOnUtc = dateOccurred;
        }
        public Guid InvoiceId { get; set; }
        public string EventType => nameof(InvoiceConfirmLinkClickedIntegrationEvent);
    }
    public class InvoiceScheduledIntegrationEvent : BaseIntegrationEvent
    {
        public InvoiceScheduledIntegrationEvent()
        {
            OccurredOnUtc = DateTime.UtcNow;
        }
        public Guid InvoiceId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmailAddress { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string InvoiceType { get; set; }
        public DateTimeOffset InvoiceStartDateTime { get; set; }
        public string EventType => nameof(InvoiceScheduledIntegrationEvent);
    }
}