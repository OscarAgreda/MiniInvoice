using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class CustomerPhoneNumber : BaseEntityEv<int>, IAggregateRoot
    {
        public CustomerPhoneNumber(Guid customerId, Guid phoneNumberId, Guid phoneNumberTypeId, bool phoneHasBeenVerified)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            PhoneNumberId = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            PhoneNumberTypeId = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
            PhoneHasBeenVerified = Guard.Against.Null(phoneHasBeenVerified, nameof(phoneHasBeenVerified));
        }

        private CustomerPhoneNumber()
        { }

        public virtual Customer Customer { get; private set; }

        public Guid CustomerId { get; private set; }

        public bool PhoneHasBeenVerified { get; private set; }

        public virtual PhoneNumber PhoneNumber { get; private set; }

        public Guid PhoneNumberId { get; private set; }

        public virtual PhoneNumberType PhoneNumberType { get; private set; }

        public Guid PhoneNumberTypeId { get; private set; }

        [Key]
        public int RowId { get; private set; }

        public void SetCustomerId(Guid customerId)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }

        public void SetPhoneNumberId(Guid phoneNumberId)
        {
            PhoneNumberId = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
        }

        public void SetPhoneNumberTypeId(Guid phoneNumberTypeId)
        {
            PhoneNumberTypeId = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
        }
    }
}