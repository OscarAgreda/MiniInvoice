using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class AddressByIdWithIncludesSpec : Specification<Address>, ISingleResultSpecification<Address>
   {
       public AddressByIdWithIncludesSpec(Guid addressId)
       {
           _ = Guard.Against.NullOrEmpty(addressId, nameof(addressId));
           _ = Query.Where(address => address.AddressId == addressId)
               .Include(x => x.City)
               .Include(x => x.Country)
               .Include(x => x.State)
               .Include(x => x.CustomerAddresses).ThenInclude(x => x.AddressType)
               .Include(x => x.CustomerAddresses).ThenInclude(x => x.Customer).AsNoTracking().AsSplitQuery()
               .EnableCache($"AddressByIdWithIncludesSpec-{addressId.ToString()}");
       }
   }
}