using System;
using MediatR;
namespace DDDInvoicingCleanL.SharedKernel
{
    public abstract class BaseIntegrationEvent : OutBoxMessage, INotification
    {
    }
}