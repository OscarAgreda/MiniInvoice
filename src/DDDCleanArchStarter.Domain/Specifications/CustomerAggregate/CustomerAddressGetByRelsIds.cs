using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerAddressByRelIdsSpec : Specification<CustomerAddress>
    {
        public CustomerAddressByRelIdsSpec(Guid addressId, Guid customerId)
        {
            Guard.Against.NullOrEmpty(addressId, nameof(addressId));
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            _ = Query.Where(customerAddress => customerAddress.AddressId == addressId && customerAddress.CustomerId == customerId).AsSplitQuery().AsNoTracking().EnableCache($"CustomerAddressByRelIdsSpec-{addressId}-{customerId}");
  }
  }
}
