using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class StateGetListSpec : Specification<State>
    {
        public StateGetListSpec()
        {
            Query.OrderBy(state => state.StateId)
      .AsNoTracking().EnableCache($"StateGetListSpec");
  }
  }
}
