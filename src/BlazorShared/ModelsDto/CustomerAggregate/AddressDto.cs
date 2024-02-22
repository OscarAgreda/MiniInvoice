using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;
namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class AddressDto : AuditBase
    {
        public Guid AddressId { get;  set; }
        [Required(ErrorMessage="Address Str is required")]
        [MaxLength(255)]
        public string AddressStr { get;  set; }
        [Required(ErrorMessage="Zip Code is required")]
        [MaxLength(255)]
        public string ZipCode { get;  set; }
        [Required(ErrorMessage="Tenant Id is required")]
        public Guid TenantId { get;  set; }
        public CityDto City { get;  set; }
        [Required(ErrorMessage="City is required")]
        public Guid CityId { get;  set; }
        public CountryDto Country { get;  set; }
        [Required(ErrorMessage="Country is required")]
        public Guid CountryId { get;  set; }
        public StateDto State { get;  set; }
        [Required(ErrorMessage="State is required")]
        public Guid StateId { get;  set; }
        public List<CustomerAddressDto> CustomerAddresses { get; set; } = new();
        public AddressDto() {}
        public AddressDto(Guid addressId, Guid cityId, Guid countryId, Guid stateId, string addressStr, string zipCode, System.Guid tenantId)
        {
                AddressId = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
                AuditEntityId = addressId.ToString();
                AuditId = Guid.NewGuid().ToString();
                AuditEntityType = GetType().Name;
                CityId = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
                CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
                StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
                AddressStr = Guard.Against.NullOrWhiteSpace(addressStr, nameof(addressStr));
                ZipCode = Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));
                TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }
    }
}
