using System;
using System.Threading;
using System.Threading.Tasks;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace FrontDesk.Domain.Handlers
{
    public class RelayAppointmentScheduledHandler : INotificationHandler<BaseIntegrationEvent>
    {
        private readonly IReadRepository<Invoice> _invoiceReadRepository;
        private readonly IApplicationMessagePublisher _messagePublisher;
        private readonly ILogger<RelayAppointmentScheduledHandler> _logger;
        public RelayAppointmentScheduledHandler(
          IReadRepository<Invoice> invoiceRepository,
          IApplicationMessagePublisher messagePublisher,
          ILogger<RelayAppointmentScheduledHandler> logger)
        {
            _invoiceReadRepository = invoiceRepository;
            _messagePublisher = messagePublisher;
            _logger = logger;
        }
        public async Task Handle(
            BaseIntegrationEvent PassedNotificationEvent,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling appointmentScheduledEvent");
        }
    }
}
