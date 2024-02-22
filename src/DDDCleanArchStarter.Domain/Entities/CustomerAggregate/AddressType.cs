using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class AddressType : BaseEntityEv<Guid>, IAggregateRoot
    {
        public AddressType(Guid addressTypeId, string addressTypeName, string? description, System.Guid tenantId)
        {
            AddressTypeId = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));
            AddressTypeName = Guard.Against.NullOrWhiteSpace(addressTypeName, nameof(addressTypeName));
            Description = description;
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private AddressType()
        { }

        [Key]
        public Guid AddressTypeId { get; private set; }

        public string AddressTypeName { get; private set; }
        public string? Description { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetAddressTypeName(string addressTypeName)
        {
            AddressTypeName = Guard.Against.NullOrEmpty(addressTypeName, nameof(addressTypeName));
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}