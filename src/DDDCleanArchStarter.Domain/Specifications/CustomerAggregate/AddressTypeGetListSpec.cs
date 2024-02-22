using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class AddressTypeGetListSpec : Specification<AddressType>
    {
        public AddressTypeGetListSpec()
        {
            Query.OrderBy(addressType => addressType.AddressTypeId)
      .AsNoTracking().EnableCache($"AddressTypeGetListSpec");
  }
  }
}
