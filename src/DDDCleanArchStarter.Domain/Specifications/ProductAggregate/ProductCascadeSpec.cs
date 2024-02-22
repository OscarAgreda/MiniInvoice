using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
    public class GetInvoiceDetailWithProductKeySpec : Specification<InvoiceDetail>
    {
        public GetInvoiceDetailWithProductKeySpec(Guid productId)
        {
            Guard.Against.NullOrEmpty(productId, nameof(productId));
            Query.Where(id => id.ProductId == productId).AsNoTracking().EnableCache($"GetInvoiceDetailWithProductKeySpec-{productId}");
        }
    }
    public class GetProductCategoryWithProductKeySpec : Specification<ProductCategory>
    {
        public GetProductCategoryWithProductKeySpec(Guid productId)
        {
            Guard.Against.NullOrEmpty(productId, nameof(productId));
            Query.Where(pc => pc.ProductId == productId).AsNoTracking().EnableCache($"GetProductCategoryWithProductKeySpec-{productId}");
        }
    }
}
