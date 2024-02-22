using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetCustomerAddressWithAddressKeySpec : Specification<CustomerAddress>
    {
        public GetCustomerAddressWithAddressKeySpec(Guid addressId)
        {
            Guard.Against.NullOrEmpty(addressId, nameof(addressId));
            Query.Where(ca => ca.AddressId == addressId).AsNoTracking().EnableCache($"GetCustomerAddressWithAddressKeySpec-{addressId}");
        }
    }
}
