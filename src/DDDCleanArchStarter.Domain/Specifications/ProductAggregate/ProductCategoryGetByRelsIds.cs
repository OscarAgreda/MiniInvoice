using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class ProductCategoryByRelIdsSpec : Specification<ProductCategory>
    {
        public ProductCategoryByRelIdsSpec(Guid tenantId, Guid productId)
        {
            Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
            Guard.Against.NullOrEmpty(productId, nameof(productId));
            _ = Query.Where(productCategory => productCategory.TenantId == tenantId && productCategory.ProductId == productId).AsSplitQuery().AsNoTracking().EnableCache($"ProductCategoryByRelIdsSpec-{tenantId}-{productId}");
  }
  }
}
