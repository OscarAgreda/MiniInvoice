using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class AddressTypeByIdWithIncludesSpec : Specification<AddressType>, ISingleResultSpecification<AddressType>

   {

       public AddressTypeByIdWithIncludesSpec(Guid addressTypeId)

       {

           _ = Guard.Against.NullOrEmpty(addressTypeId, nameof(addressTypeId));

           _ = Query.Where(addressType => addressType.AddressTypeId == addressTypeId).AsNoTracking().AsSplitQuery()

               .EnableCache($"AddressTypeByIdWithIncludesSpec-{addressTypeId.ToString()}");

       }

   }

}