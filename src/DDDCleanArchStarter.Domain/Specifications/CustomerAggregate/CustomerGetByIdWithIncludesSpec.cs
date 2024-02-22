using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class CustomerByIdWithIncludesSpec : Specification<Customer>, ISingleResultSpecification<Customer>
   {
       public CustomerByIdWithIncludesSpec(Guid customerId)
       {
           _ = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
           _ = Query.Where(customer => customer.CustomerId == customerId)
               .Include(x => x.CustomerAccounts).ThenInclude(x => x.Account)
               .Include(x => x.CustomerAddresses).ThenInclude(x => x.Address)
               .Include(x => x.CustomerAddresses).ThenInclude(x => x.AddressType)
               .Include(x => x.CustomerPhoneNumbers).ThenInclude(x => x.PhoneNumber)
               .Include(x => x.CustomerPhoneNumbers).ThenInclude(x => x.PhoneNumberType).AsNoTracking().AsSplitQuery()
               .EnableCache($"CustomerByIdWithIncludesSpec-{customerId.ToString()}");
       }
   }
}