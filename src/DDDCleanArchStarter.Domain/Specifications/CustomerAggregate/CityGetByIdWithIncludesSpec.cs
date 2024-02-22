using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class CityByIdWithIncludesSpec : Specification<City>, ISingleResultSpecification<City>
   {
       public CityByIdWithIncludesSpec(Guid cityId)
       {
           _ = Guard.Against.NullOrEmpty(cityId, nameof(cityId));
           _ = Query.Where(city => city.CityId == cityId)
               .Include(x => x.State)
               .Include(x => x.Addresses).ThenInclude(x => x.Country)
               .Include(x => x.Addresses).ThenInclude(x => x.State).AsNoTracking().AsSplitQuery()
               .EnableCache($"CityByIdWithIncludesSpec-{cityId.ToString()}");
       }
   }
}