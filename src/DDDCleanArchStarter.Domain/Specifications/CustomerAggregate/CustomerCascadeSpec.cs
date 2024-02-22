using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetCustomerAccountWithCustomerKeySpec : Specification<CustomerAccount>
    {
        public GetCustomerAccountWithCustomerKeySpec(Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Query.Where(ca => ca.CustomerId == customerId).AsNoTracking().EnableCache($"GetCustomerAccountWithCustomerKeySpec-{customerId}");
        }
    }
    public class GetCustomerAddressWithCustomerKeySpec : Specification<CustomerAddress>
    {
        public GetCustomerAddressWithCustomerKeySpec(Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Query.Where(ca => ca.CustomerId == customerId).AsNoTracking().EnableCache($"GetCustomerAddressWithCustomerKeySpec-{customerId}");
        }
    }
    public class GetCustomerPhoneNumberWithCustomerKeySpec : Specification<CustomerPhoneNumber>
    {
        public GetCustomerPhoneNumberWithCustomerKeySpec(Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Query.Where(cpn => cpn.CustomerId == customerId).AsNoTracking().EnableCache($"GetCustomerPhoneNumberWithCustomerKeySpec-{customerId}");
        }
    }
}
