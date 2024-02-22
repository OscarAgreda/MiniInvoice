using System;
using JetBrains.Annotations;
namespace DDDInvoicingCleanL.SharedKernel
{
    public class OutBoxMessage
    {
        public Guid EventId { get; protected set; }
        public string? Consumer { get; set; }
        public string EventType { get; set; }
        public string EntityNameType { get; set; }
        public string? ActionOnMessageReceived { get; set; }
        public string? Content { get; set; }
        public DateTime? OccurredOnUtc { get; set; }
        public DateTime? ProcessedOnUtc { get; set; }
    }
}