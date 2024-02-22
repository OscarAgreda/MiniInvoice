using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Country : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<Address> _addresses = new();

        private readonly List<State> _states = new();

        public Country(Guid countryId, string countryName, string countryCode, System.Guid tenantId)
        {
            CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
            CountryName = Guard.Against.NullOrWhiteSpace(countryName, nameof(countryName));
            CountryCode = Guard.Against.NullOrWhiteSpace(countryCode, nameof(countryCode));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private Country()
        { }

        public IEnumerable<Address> Addresses => _addresses.AsReadOnly();

        public string CountryCode { get; private set; }

        [Key]
        public Guid CountryId { get; private set; }

        public string CountryName { get; private set; }
        public IEnumerable<State> States => _states.AsReadOnly();
        public Guid TenantId { get; private set; }

        public void SetCountryCode(string countryCode)
        {
            CountryCode = Guard.Against.NullOrEmpty(countryCode, nameof(countryCode));
        }

        public void SetCountryName(string countryName)
        {
            CountryName = Guard.Against.NullOrEmpty(countryName, nameof(countryName));
        }
    }
}