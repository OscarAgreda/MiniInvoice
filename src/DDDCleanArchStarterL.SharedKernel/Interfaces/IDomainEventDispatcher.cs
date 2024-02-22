using System.Collections.Generic;
using System.Threading.Tasks;
namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
    }
}