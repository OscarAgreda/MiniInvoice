using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class CustomerAddress : BaseEntityEv<int>, IAggregateRoot
    {
        public CustomerAddress(Guid addressId, Guid addressTypeId, Guid customerId)
        {
            AddressId = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
            AddressTypeId = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }

        private CustomerAddress()
        { }

        public virtual Address Address { get; private set; }

        public Guid AddressId { get; private set; }

        public virtual AddressType AddressType { get; private set; }

        public Guid AddressTypeId { get; private set; }

        public virtual Customer Customer { get; private set; }

        public Guid CustomerId { get; private set; }

        [Key]
        public int RowId { get; private set; }

        public void SetAddressId(Guid addressId)
        {
            AddressId = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
        }

        public void SetAddressTypeId(Guid addressTypeId)
        {
            AddressTypeId = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));
        }

        public void SetCustomerId(Guid customerId)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
        }
    }
}