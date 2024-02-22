using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDCleanArchStarter.Api.ApplicationEvents
{
    public class NamedEntityCreatedEvent : IApplicationEvent
    {
        public NamedEntityCreatedEvent(NamedEntity entity, string eventType)
        {
            Entity = entity;
            EventType = eventType;
        }
        public NamedEntity Entity { get; set; }
        public string EventType { get; set; }
    }
}