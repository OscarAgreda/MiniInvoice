using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetAccountAdjustmentWithAccountKeySpec : Specification<AccountAdjustment>
    {
        public GetAccountAdjustmentWithAccountKeySpec(Guid accountId)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Query.Where(aa => aa.AccountId == accountId).AsNoTracking().EnableCache($"GetAccountAdjustmentWithAccountKeySpec-{accountId}");
        }
    }
    public class GetCustomerAccountWithAccountKeySpec : Specification<CustomerAccount>
    {
        public GetCustomerAccountWithAccountKeySpec(Guid accountId)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Query.Where(ca => ca.AccountId == accountId).AsNoTracking().EnableCache($"GetCustomerAccountWithAccountKeySpec-{accountId}");
        }
    }
    public class GetInvoiceWithAccountKeySpec : Specification<Invoice>
    {
        public GetInvoiceWithAccountKeySpec(Guid accountId)
        {
            Guard.Against.NullOrEmpty(accountId, nameof(accountId));
            Query.Where(i => i.AccountId == accountId).AsNoTracking().EnableCache($"GetInvoiceWithAccountKeySpec-{accountId}");
        }
    }
}
