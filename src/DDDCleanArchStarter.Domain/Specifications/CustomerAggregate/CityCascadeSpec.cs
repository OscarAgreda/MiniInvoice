using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetAddressWithCityKeySpec : Specification<Address>
    {
        public GetAddressWithCityKeySpec(Guid cityId)
        {
            Guard.Against.NullOrEmpty(cityId, nameof(cityId));
            Query.Where(a => a.CityId == cityId).AsNoTracking().EnableCache($"GetAddressWithCityKeySpec-{cityId}");
        }
    }
}
