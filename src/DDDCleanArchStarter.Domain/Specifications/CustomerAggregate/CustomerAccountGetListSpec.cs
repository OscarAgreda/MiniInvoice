using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerAccountGetListSpec : Specification<CustomerAccount>
    {
        public CustomerAccountGetListSpec()
        {
            Query.OrderBy(customerAccount => customerAccount.RowId)
      .AsNoTracking().EnableCache($"CustomerAccountGetListSpec");
  }
  }
}
