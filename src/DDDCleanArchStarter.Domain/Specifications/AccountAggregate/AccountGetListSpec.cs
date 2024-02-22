using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class AccountGetListSpec : Specification<Account>
    {
        public AccountGetListSpec()
        {
            Query.OrderBy(account => account.AccountId)
      .AsNoTracking().EnableCache($"AccountGetListSpec");
  }
  }
}
