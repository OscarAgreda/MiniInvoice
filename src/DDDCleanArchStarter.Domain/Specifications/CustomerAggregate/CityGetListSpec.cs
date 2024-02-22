using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CityGetListSpec : Specification<City>
    {
        public CityGetListSpec()
        {
            Query.OrderBy(city => city.CityId)
      .AsNoTracking().EnableCache($"CityGetListSpec");
  }
  }
}
