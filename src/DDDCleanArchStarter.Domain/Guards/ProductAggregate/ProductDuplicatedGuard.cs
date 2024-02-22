using System.Collections.Generic;
using System.Linq;
using DDDInvoicingClean.Domain.Entities;
using DDDInvoicingClean.Domain.Exceptions;
namespace Ardalis.GuardClauses
{
    public static class ProductGuardExtensions
    {
        public static void DuplicateProduct(this IGuardClause guardClause, IEnumerable<Product> existingProducts, Product newProduct, string parameterName)
        {
            if (existingProducts.Any(a => a.ProductId == newProduct.ProductId))
            {
                throw new DuplicateProductException("Cannot add duplicate product.", parameterName);
            }
        }
    }
}
