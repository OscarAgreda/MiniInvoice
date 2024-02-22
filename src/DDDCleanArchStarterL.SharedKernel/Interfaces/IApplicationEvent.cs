namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public interface IApplicationEvent
    {
        string EventType { get; }
    }
}