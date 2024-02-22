using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class ProductTypeNotFoundException : Exception
  {
    public ProductTypeNotFoundException(string message) : base(message)
    {
    }
    public ProductTypeNotFoundException(int productTypeId) : base($"No productType with id {productTypeId} found.")
    {
    }
  }
}
