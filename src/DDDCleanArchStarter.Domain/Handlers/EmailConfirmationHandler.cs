

using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using DDDCleanArchStarter.Domain.Events.IntegrationEvents;
using DDDCleanArchStarter.Domain.Interfaces;
using DDDCleanArchStarterL.SharedKernel.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrontDesk.Domain.Handlers
{
    /// <summary>
    /// This handler responds to incoming messages saying a user has confirmed an appointment
    /// </summary>
    public class EmailConfirmationHandler : INotificationHandler<InvoiceConfirmLinkClickedIntegrationEvent>
    {
        private readonly IRepository<Schedule<>> _scheduleRepository;
        private readonly IApplicationSettings _settings;
        private readonly ILogger<EmailConfirmationHandler> _logger;

        public EmailConfirmationHandler(IRepository<Schedule> scheduleRepository,
          IApplicationSettings settings,
          ILogger<EmailConfirmationHandler> logger)
        {
            _scheduleRepository = scheduleRepository;
            _settings = settings;
            _logger = logger;
        }

        public async Task Handle(InvoiceConfirmLinkClickedIntegrationEvent appointmentConfirmedEvent,
          CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling appointment confirmation: {appointmentConfirmedEvent.AppointmentId}");

            var scheduleSpec = new ScheduleForClinicAndDateWithAppointmentsSpec(_settings.ClinicId, _settings.TestDate);
            // Note: In this demo this only works for appointments scheduled on TestDate
            var schedule = (await _scheduleRepository.GetBySpecAsync(scheduleSpec));
            Guard.Against.Null(schedule, nameof(Schedule));

            var appointmentToConfirm = schedule.Appointments.FirstOrDefault(a => a.Id == appointmentConfirmedEvent.AppointmentId);

            appointmentToConfirm.Confirm(appointmentConfirmedEvent.DateOccurred);

            await _scheduleRepository.UpdateAsync(schedule);
        }
    }
}
