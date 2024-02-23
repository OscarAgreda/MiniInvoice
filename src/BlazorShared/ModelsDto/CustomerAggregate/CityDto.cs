using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CityDto : AuditBase
    {
        public CityDto()
        { }

        public CityDto(Guid cityId, Guid stateId, string cityName, System.Guid tenantId)
        {
            CityId = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
            AuditEntityId = cityId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            CityName = Guard.Against.NullOrWhiteSpace(cityName, nameof(cityName));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        public List<AddressDto> Addresses { get; set; } = new();
        public Guid CityId { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [MaxLength(255)]
        public string CityName { get; set; }

        public StateDto State { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Guid StateId { get; set; }

        [Required(ErrorMessage = "Tenant Id is required")]
        public Guid TenantId { get; set; }
    }
}