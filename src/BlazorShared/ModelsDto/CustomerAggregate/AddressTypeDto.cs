using System;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class AddressTypeDto : AuditBase
    {
        public AddressTypeDto()
        { }

        public AddressTypeDto(Guid addressTypeId, string addressTypeName, string? description, System.Guid tenantId)
        {
            AddressTypeId = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));
            AuditEntityId = addressTypeId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            AddressTypeName = Guard.Against.NullOrWhiteSpace(addressTypeName, nameof(addressTypeName));
            Description = description;
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        public Guid AddressTypeId { get; set; }

        [Required(ErrorMessage = "Address Type Name is required")]
        [MaxLength(255)]
        public string AddressTypeName { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Tenant Id is required")]
        public Guid TenantId { get; set; }
    }
}