using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetAddressWithCountryKeySpec : Specification<Address>
    {
        public GetAddressWithCountryKeySpec(Guid countryId)
        {
            Guard.Against.NullOrEmpty(countryId, nameof(countryId));
            Query.Where(a => a.CountryId == countryId).AsNoTracking().EnableCache($"GetAddressWithCountryKeySpec-{countryId}");
        }
    }
    public class GetStateWithCountryKeySpec : Specification<State>
    {
        public GetStateWithCountryKeySpec(Guid countryId)
        {
            Guard.Against.NullOrEmpty(countryId, nameof(countryId));
            Query.Where(s => s.CountryId == countryId).AsNoTracking().EnableCache($"GetStateWithCountryKeySpec-{countryId}");
        }
    }
}
