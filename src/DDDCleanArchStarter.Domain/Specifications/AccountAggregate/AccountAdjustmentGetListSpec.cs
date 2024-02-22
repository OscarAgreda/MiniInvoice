using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class AccountAdjustmentGetListSpec : Specification<AccountAdjustment>
    {
        public AccountAdjustmentGetListSpec()
        {
            Query.OrderBy(accountAdjustment => accountAdjustment.AccountAdjustmentId)
      .AsNoTracking().EnableCache($"AccountAdjustmentGetListSpec");
  }
  }
}
