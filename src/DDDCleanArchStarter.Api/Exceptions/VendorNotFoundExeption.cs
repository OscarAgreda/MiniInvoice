using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class VendorNotFoundException : Exception
  {
    public VendorNotFoundException(string message) : base(message)
    {
    }
    public VendorNotFoundException(int vendorId) : base($"No vendor with id {vendorId} found.")
    {
    }
  }
}
