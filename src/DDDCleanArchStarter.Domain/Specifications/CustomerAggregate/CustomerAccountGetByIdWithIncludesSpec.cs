using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class CustomerAccountByIdWithIncludesSpec : Specification<CustomerAccount>, ISingleResultSpecification<CustomerAccount>

   {

       public CustomerAccountByIdWithIncludesSpec(Guid customerId)

       {

           _ = Guard.Against.NullOrEmpty(customerId, nameof(customerId));

           _ = Query.Where(customerAccount => customerAccount.CustomerId == customerId)
               .Include(x => x.Account)
               .Include(x => x.Customer).AsNoTracking().AsSplitQuery()

               .EnableCache($"CustomerAccountByIdWithIncludesSpec-{customerId.ToString()}");

       }

   }

}