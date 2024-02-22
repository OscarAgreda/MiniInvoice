using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class AccountByIdWithIncludesSpec : Specification<Account>, ISingleResultSpecification<Account>
   {
       public AccountByIdWithIncludesSpec(Guid accountId)
       {
           _ = Guard.Against.NullOrEmpty(accountId, nameof(accountId));
           _ = Query.Where(account => account.AccountId == accountId)
               .Include(x => x.CustomerAccounts).ThenInclude(x => x.Customer).AsNoTracking().AsSplitQuery()
               .EnableCache($"AccountByIdWithIncludesSpec-{accountId.ToString()}");
       }
   }
}