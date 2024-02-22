using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class City : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<Address> _addresses = new();

        public City(Guid cityId, Guid stateId, string cityName, System.Guid tenantId)
        {
            CityId = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            CityName = Guard.Against.NullOrWhiteSpace(cityName, nameof(cityName));
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
        }

        private City()
        { }

        public IEnumerable<Address> Addresses => _addresses.AsReadOnly();

        [Key]
        public Guid CityId { get; private set; }

        public string CityName { get; private set; }
        public virtual State State { get; private set; }
        public Guid StateId { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetCityName(string cityName)
        {
            CityName = Guard.Against.NullOrEmpty(cityName, nameof(cityName));
        }

        public void SetStateId(Guid stateId)
        {
            StateId = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
        }
    }
}