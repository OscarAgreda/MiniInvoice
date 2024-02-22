using System;
using System.Collections.Generic;
using JetBrains.Annotations;
namespace DDDInvoicingCleanL.SharedKernel
{
    public abstract class BaseEntityEv<TId>
    {
        [CanBeNull] public List<BaseDomainEvent> Events = new ();
    }
}