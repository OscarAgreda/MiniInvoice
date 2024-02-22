using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CustomerGetListSpec : Specification<Customer>
    {
        public CustomerGetListSpec()
        {
            Query.OrderBy(customer => customer.CustomerId)
      .AsNoTracking().EnableCache($"CustomerGetListSpec");
  }
  }
}
