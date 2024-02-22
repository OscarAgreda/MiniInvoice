using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class ProductCategoryNotFoundException : Exception
  {
    public ProductCategoryNotFoundException(string message) : base(message)
    {
    }
    public ProductCategoryNotFoundException(Int32  rowId) : base($"No productCategory with id {rowId} found.")
    {
    }
  }
}
