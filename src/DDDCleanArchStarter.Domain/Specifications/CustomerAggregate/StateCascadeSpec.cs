using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetAddressWithStateKeySpec : Specification<Address>
    {
        public GetAddressWithStateKeySpec(Guid stateId)
        {
            Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            Query.Where(a => a.StateId == stateId).AsNoTracking().EnableCache($"GetAddressWithStateKeySpec-{stateId}");
        }
    }
    public class GetCityWithStateKeySpec : Specification<City>
    {
        public GetCityWithStateKeySpec(Guid stateId)
        {
            Guard.Against.NullOrEmpty(stateId, nameof(stateId));
            Query.Where(c => c.StateId == stateId).AsNoTracking().EnableCache($"GetCityWithStateKeySpec-{stateId}");
        }
    }
}
