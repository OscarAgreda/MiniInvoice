using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerAddressGetListSpec : Specification<CustomerAddress>
    {
        public CustomerAddressGetListSpec()
        {
            Query.OrderBy(customerAddress => customerAddress.RowId)
      .AsNoTracking().EnableCache($"CustomerAddressGetListSpec");
  }
  }
}
