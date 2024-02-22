using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class CountryGetListSpec : Specification<Country>
    {
        public CountryGetListSpec()
        {
            Query.OrderBy(country => country.CountryId)
      .AsNoTracking().EnableCache($"CountryGetListSpec");
  }
  }
}
