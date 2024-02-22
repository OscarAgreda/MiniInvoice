using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerAccountByRelIdsSpec : Specification<CustomerAccount>
    {
        public CustomerAccountByRelIdsSpec(Guid accountId, Guid customerId)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            _ = Query.Where(customerAccount => customerAccount.AccountId == accountId && customerAccount.CustomerId == customerId).AsSplitQuery().AsNoTracking().EnableCache($"CustomerAccountByRelIdsSpec-{accountId}-{customerId}");
  }
  }
}
