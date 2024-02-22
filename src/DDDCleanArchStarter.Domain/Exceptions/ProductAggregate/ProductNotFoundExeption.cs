using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class ProductNotFoundException : Exception
  {
    public ProductNotFoundException(string message) : base(message)
    {
    }
    public ProductNotFoundException(Guid  productId) : base($"No product with id {productId} found.")
    {
    }
  }
}
