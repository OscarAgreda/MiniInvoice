using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class StateDto : AuditBase
    {
        public Guid StateId { get;  set; }
        [Required(ErrorMessage="State Name is required")]
        [MaxLength(255)]
        public string StateName { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public CountryDto Country { get;  set; }
        [Required(ErrorMessage="Country is required")]
        public Guid CountryId { get;  set; }
        public List<AddressDto> Addresses { get; set; } = new();
        public List<CityDto> Cities { get; set; } = new();
        public StateDto() {}
        public StateDto(Guid stateId, Guid countryId, string stateName, System.Guid tenantId)
        {
                StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
                AuditEntityId = stateId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
                StateName = Guard.Against.NullOrWhiteSpace(stateName, nameof(stateName));
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
