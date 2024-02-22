using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class PhoneNumberTypeGetListSpec : Specification<PhoneNumberType>
    {
        public PhoneNumberTypeGetListSpec()
        {
            Query.OrderBy(phoneNumberType => phoneNumberType.PhoneNumberTypeId)
      .AsNoTracking().EnableCache($"PhoneNumberTypeGetListSpec");
  }
  }
}
