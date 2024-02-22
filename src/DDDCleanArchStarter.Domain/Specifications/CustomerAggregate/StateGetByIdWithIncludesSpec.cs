using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class StateByIdWithIncludesSpec : Specification<State>, ISingleResultSpecification<State>
   {
       public StateByIdWithIncludesSpec(Guid stateId)
       {
           _ = Guard.Against.NullOrEmpty(stateId, nameof(stateId));
           _ = Query.Where(state => state.StateId == stateId)
               .Include(x => x.Country)
               .Include(x => x.Addresses).ThenInclude(x => x.City)
               .Include(x => x.Addresses).ThenInclude(x => x.Country).AsNoTracking().AsSplitQuery()
               .EnableCache($"StateByIdWithIncludesSpec-{stateId.ToString()}");
       }
   }
}