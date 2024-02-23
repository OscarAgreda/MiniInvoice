using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class CountryByIdWithIncludesSpec : Specification<Country>, ISingleResultSpecification<Country>

   {

       public CountryByIdWithIncludesSpec(Guid countryId)

       {

           _ = Guard.Against.NullOrEmpty(countryId, nameof(countryId));

           _ = Query.Where(country => country.CountryId == countryId)
               .Include(x => x.Addresses).ThenInclude(x => x.City)
               .Include(x => x.Addresses).ThenInclude(x => x.State).AsNoTracking().AsSplitQuery()

               .EnableCache($"CountryByIdWithIncludesSpec-{countryId.ToString()}");

       }

   }

}