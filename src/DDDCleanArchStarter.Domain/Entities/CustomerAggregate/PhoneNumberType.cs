using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class PhoneNumberType : BaseEntityEv<Guid>, IAggregateRoot
    {
        public PhoneNumberType(Guid phoneNumberTypeId, string phoneNumberTypeName, string? description, System.Guid tenantId)
        {
            PhoneNumberTypeId = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
            PhoneNumberTypeName = Guard.Against.NullOrWhiteSpace(phoneNumberTypeName, nameof(phoneNumberTypeName));
            Description = description;
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private PhoneNumberType()
        { }

        public string? Description { get; private set; }

        [Key]
        public Guid PhoneNumberTypeId { get; private set; }

        public string PhoneNumberTypeName { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetPhoneNumberTypeName(string phoneNumberTypeName)
        {
            PhoneNumberTypeName = Guard.Against.NullOrEmpty(phoneNumberTypeName, nameof(phoneNumberTypeName));
        }
    }
}