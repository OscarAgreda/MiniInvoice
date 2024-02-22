using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace DDDInvoicingCleanL.SharedKernel
{
    public abstract class BaseDomainEvent : OutBoxMessage, INotification
    {
    }
}