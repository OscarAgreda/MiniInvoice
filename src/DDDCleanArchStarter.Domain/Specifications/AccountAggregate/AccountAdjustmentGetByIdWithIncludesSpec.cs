using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class AccountAdjustmentByIdWithIncludesSpec : Specification<AccountAdjustment>, ISingleResultSpecification<AccountAdjustment>
   {
       public AccountAdjustmentByIdWithIncludesSpec(Guid accountAdjustmentId)
       {
           _ = Guard.Against.NullOrEmpty(accountAdjustmentId, nameof(accountAdjustmentId));
           _ = Query.Where(accountAdjustment => accountAdjustment.AccountAdjustmentId == accountAdjustmentId)
               .Include(x => x.Account).AsNoTracking().AsSplitQuery()
               .EnableCache($"AccountAdjustmentByIdWithIncludesSpec-{accountAdjustmentId.ToString()}");
       }
   }
}