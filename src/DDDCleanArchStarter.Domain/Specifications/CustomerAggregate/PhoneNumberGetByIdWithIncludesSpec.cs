using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class PhoneNumberByIdWithIncludesSpec : Specification<PhoneNumber>, ISingleResultSpecification<PhoneNumber>
   {
       public PhoneNumberByIdWithIncludesSpec(Guid phoneNumberId)
       {
           _ = Guard.Against.NullOrEmpty(phoneNumberId, nameof(phoneNumberId));
           _ = Query.Where(phoneNumber => phoneNumber.PhoneNumberId == phoneNumberId)
               .Include(x => x.CustomerPhoneNumbers).ThenInclude(x => x.Customer)
               .Include(x => x.CustomerPhoneNumbers).ThenInclude(x => x.PhoneNumberType).AsNoTracking().AsSplitQuery()
               .EnableCache($"PhoneNumberByIdWithIncludesSpec-{phoneNumberId.ToString()}");
       }
   }
}