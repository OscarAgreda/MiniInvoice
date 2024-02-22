using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class State : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<Address> _addresses = new();

        private readonly List<City> _cities = new();

        public State(Guid stateId, Guid countryId, string stateName, System.Guid tenantId)
        {
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
            StateName = Guard.Against.NullOrWhiteSpace(stateName, nameof(stateName));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private State()
        { }

        public IEnumerable<Address> Addresses => _addresses.AsReadOnly();

        public IEnumerable<City> Cities => _cities.AsReadOnly();

        public virtual Country Country { get; private set; }

        public Guid CountryId { get; private set; }

        [Key]
        public Guid StateId { get; private set; }

        public string StateName { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetCountryId(Guid countryId)
        {
            CountryId = Guard.Against.NullOrEmpty(countryId, nameof(countryId));
        }

        public void SetStateName(string stateName)
        {
            StateName = Guard.Against.NullOrEmpty(stateName, nameof(stateName));
        }
    }
}