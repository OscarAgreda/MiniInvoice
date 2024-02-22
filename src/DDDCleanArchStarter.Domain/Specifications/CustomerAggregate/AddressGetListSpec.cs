using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class AddressGetListSpec : Specification<Address>
    {
        public AddressGetListSpec()
        {
            Query.OrderBy(address => address.AddressId)
      .AsNoTracking().EnableCache($"AddressGetListSpec");
  }
  }
}
