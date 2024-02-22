using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerPhoneNumberByRelIdsSpec : Specification<CustomerPhoneNumber>
    {
        public CustomerPhoneNumberByRelIdsSpec(Guid customerId, Guid phoneNumberId, bool phoneHasBeenVerified)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
            Guard.Against.Null(phoneHasBeenVerified, nameof(phoneHasBeenVerified));
            _ = Query.Where(customerPhoneNumber => customerPhoneNumber.CustomerId == customerId && customerPhoneNumber.PhoneNumberId == phoneNumberId && customerPhoneNumber.PhoneHasBeenVerified == phoneHasBeenVerified).AsSplitQuery().AsNoTracking().EnableCache($"CustomerPhoneNumberByRelIdsSpec-{customerId}-{phoneNumberId}-{phoneHasBeenVerified}");
  }
  }
}
