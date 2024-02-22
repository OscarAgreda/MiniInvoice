using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class CustomerPhoneNumberByIdWithIncludesSpec : Specification<CustomerPhoneNumber>, ISingleResultSpecification<CustomerPhoneNumber>
   {
       public CustomerPhoneNumberByIdWithIncludesSpec(Guid customerId)
       {
           _ = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
           _ = Query.Where(customerPhoneNumber => customerPhoneNumber.CustomerId == customerId)
               .Include(x => x.Customer)
               .Include(x => x.PhoneNumber)
               .Include(x => x.PhoneNumberType).AsNoTracking().AsSplitQuery()
               .EnableCache($"CustomerPhoneNumberByIdWithIncludesSpec-{customerId.ToString()}");
       }
   }
}