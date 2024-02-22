using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class CustomerAddressByIdWithIncludesSpec : Specification<CustomerAddress>, ISingleResultSpecification<CustomerAddress>
   {
       public CustomerAddressByIdWithIncludesSpec(Guid customerId)
       {
           _ = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
           _ = Query.Where(customerAddress => customerAddress.CustomerId == customerId)
               .Include(x => x.Address)
               .Include(x => x.AddressType)
               .Include(x => x.Customer).AsNoTracking().AsSplitQuery()
               .EnableCache($"CustomerAddressByIdWithIncludesSpec-{customerId.ToString()}");
       }
   }
}