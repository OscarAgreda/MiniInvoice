using System;
using System.Linq;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class ProductCategoryGetListSpec : Specification<ProductCategory>
    {
        public ProductCategoryGetListSpec()
        {
            Query.OrderBy(productCategory => productCategory.RowId)
      .AsNoTracking().EnableCache($"ProductCategoryGetListSpec");
  }
  }
}
