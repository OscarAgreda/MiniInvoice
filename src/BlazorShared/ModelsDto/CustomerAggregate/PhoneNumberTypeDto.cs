using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class PhoneNumberTypeDto : AuditBase
    {
        public Guid PhoneNumberTypeId { get;  set; }
        [Required(ErrorMessage="Phone Number Type Name is required")]
        [MaxLength(255)]
        public string PhoneNumberTypeName { get;  set; }
        [MaxLength(255)]
        public string? Description { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public PhoneNumberTypeDto() {}
        public PhoneNumberTypeDto(Guid phoneNumberTypeId, string phoneNumberTypeName, string? description, System.Guid tenantId)
        {
                PhoneNumberTypeId = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
                AuditEntityId = phoneNumberTypeId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                PhoneNumberTypeName = Guard.Against.NullOrWhiteSpace(phoneNumberTypeName, nameof(phoneNumberTypeName));
                Description = description;
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
