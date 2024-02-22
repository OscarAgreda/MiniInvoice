using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CountryDto : AuditBase
    {
        public Guid CountryId { get;  set; }
        [Required(ErrorMessage="Country Name is required")]
        [MaxLength(255)]
        public string CountryName { get;  set; }
        [Required(ErrorMessage="Country Code is required")]
        [MaxLength(255)]
        public string CountryCode { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public List<AddressDto> Addresses { get; set; } = new();
        public List<StateDto> States { get; set; } = new();
        public CountryDto() {}
        public CountryDto(Guid countryId, string countryName, string countryCode, System.Guid tenantId)
        {
                CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
                AuditEntityId = countryId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                CountryName = Guard.Against.NullOrWhiteSpace(countryName, nameof(countryName));
                CountryCode = Guard.Against.NullOrWhiteSpace(countryCode, nameof(countryCode));
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
