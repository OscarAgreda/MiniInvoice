using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CustomerAddressNotFoundException : Exception
  {
    public CustomerAddressNotFoundException(string message) : base(message)
    {
    }
    public CustomerAddressNotFoundException(Int32  rowId) : base($"No customerAddress with id {rowId} found.")
    {
    }
  }
}
