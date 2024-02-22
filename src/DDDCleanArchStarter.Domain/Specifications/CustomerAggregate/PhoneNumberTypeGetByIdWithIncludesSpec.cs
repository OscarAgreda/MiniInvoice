using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class PhoneNumberTypeByIdWithIncludesSpec : Specification<PhoneNumberType>, ISingleResultSpecification<PhoneNumberType>
   {
       public PhoneNumberTypeByIdWithIncludesSpec(Guid phoneNumberTypeId)
       {
           _ = Guard.Against.NullOrEmpty(phoneNumberTypeId, nameof(phoneNumberTypeId));
           _ = Query.Where(phoneNumberType => phoneNumberType.PhoneNumberTypeId == phoneNumberTypeId).AsNoTracking().AsSplitQuery()
               .EnableCache($"PhoneNumberTypeByIdWithIncludesSpec-{phoneNumberTypeId.ToString()}");
       }
   }
}