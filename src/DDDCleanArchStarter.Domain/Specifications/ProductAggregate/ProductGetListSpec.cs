using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class ProductGetListSpec : Specification<Product>
    {
        public ProductGetListSpec()
        {
            Query.OrderBy(product => product.ProductId)
      .AsNoTracking().EnableCache($"ProductGetListSpec");
  }
  }
}
