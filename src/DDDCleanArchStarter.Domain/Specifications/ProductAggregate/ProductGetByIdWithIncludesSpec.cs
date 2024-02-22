using System;
using System.Linq;
using Ardalis.GuardClauses;
using Ardalis.Specification;
using DDDInvoicingClean.Domain.Entities;
namespace DDDInvoicingClean.Domain.Specifications
{
   public class ProductByIdWithIncludesSpec : Specification<Product>, ISingleResultSpecification<Product>
   {
       public ProductByIdWithIncludesSpec(Guid productId)
       {
           _ = Guard.Against.NullOrEmpty(productId, nameof(productId));
           _ = Query.Where(product => product.ProductId == productId)
               .Include(x => x.InvoiceDetails).ThenInclude(x => x.Invoice).AsNoTracking().AsSplitQuery()
               .EnableCache($"ProductByIdWithIncludesSpec-{productId.ToString()}");
       }
   }
}