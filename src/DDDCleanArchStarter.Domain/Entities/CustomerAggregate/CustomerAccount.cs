using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class CustomerAccount : BaseEntityEv<int>, IAggregateRoot
    {
        public CustomerAccount(Guid accountId, Guid customerId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }

        private CustomerAccount()
        { }

        public virtual Account Account { get; private set; }

        public Guid AccountId { get; private set; }

        public virtual Customer Customer { get; private set; }

        public Guid CustomerId { get; private set; }

        [Key]
        public int RowId { get; private set; }

        public void SetAccountId(Guid accountId)
        {
            AccountId = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
        }

        public void SetCustomerId(Guid customerId)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }
    }
}