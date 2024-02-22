using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class PhoneNumberGetListSpec : Specification<PhoneNumber>
    {
        public PhoneNumberGetListSpec()
        {
            Query.OrderBy(phoneNumber => phoneNumber.PhoneNumberId)
      .AsNoTracking().EnableCache($"PhoneNumberGetListSpec");
  }
  }
}
