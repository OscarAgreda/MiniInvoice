using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerPhoneNumberGetListSpec : Specification<CustomerPhoneNumber>
    {
        public CustomerPhoneNumberGetListSpec()
        {
            Query.OrderBy(customerPhoneNumber => customerPhoneNumber.RowId)
      .AsNoTracking().EnableCache($"CustomerPhoneNumberGetListSpec");
  }
  }
}
