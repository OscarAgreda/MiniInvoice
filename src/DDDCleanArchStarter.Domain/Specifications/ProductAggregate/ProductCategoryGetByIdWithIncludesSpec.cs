using System;

using System.Linq;

using Ardalis.GuardClauses;

using Ardalis.Specification;

using DDDInvoicingClean.Domain.Entities;

namespace DDDInvoicingClean.Domain.Specifications

{

   public class ProductCategoryByIdWithIncludesSpec : Specification<ProductCategory>, ISingleResultSpecification<ProductCategory>

   {

       public ProductCategoryByIdWithIncludesSpec(Guid productId)

       {

           _ = Guard.Against.NullOrEmpty(productId, nameof(productId));

           _ = Query.Where(productCategory => productCategory.ProductId == productId)
               .Include(x => x.Product).AsNoTracking().AsSplitQuery()

               .EnableCache($"ProductCategoryByIdWithIncludesSpec-{productId.ToString()}");

       }

   }

}