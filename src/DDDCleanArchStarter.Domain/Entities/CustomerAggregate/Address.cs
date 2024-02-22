using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Address : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<CustomerAddress> _customerAddresses = new();

        public Address(Guid addressId, Guid cityId, Guid countryId, Guid stateId, string addressStr, string zipCode, System.Guid tenantId)
        {
            AddressId = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
            CityId = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
            CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            AddressStr = Guard.Against.NullOrWhiteSpace(addressStr, nameof(addressStr));
            ZipCode = Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private Address()
        { }

        [Key]
        public Guid AddressId { get; private set; }

        public string AddressStr { get; private set; }
        public virtual City City { get; private set; }
        public Guid CityId { get; private set; }
        public virtual Country Country { get; private set; }
        public Guid CountryId { get; private set; }
        public IEnumerable<CustomerAddress> CustomerAddresses => _customerAddresses.AsReadOnly();
        public virtual State State { get; private set; }
        public Guid StateId { get; private set; }
        public Guid TenantId { get; private set; }
        public string ZipCode { get; private set; }

        public void SetAddressStr(string addressStr)
        {
            AddressStr = Guard.Against.NullOrEmpty(addressStr, nameof(addressStr));
        }

        public void SetCityId(Guid cityId)
        {
            CityId = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
        }

        public void SetCountryId(Guid countryId)
        {
            CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
        }

        public void SetStateId(Guid stateId)
        {
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
        }

        public void SetZipCode(string zipCode)
        {
            ZipCode = Guard.Against.NullOrEmpty(zipCode, nameof(zipCode));
        }
    }
}