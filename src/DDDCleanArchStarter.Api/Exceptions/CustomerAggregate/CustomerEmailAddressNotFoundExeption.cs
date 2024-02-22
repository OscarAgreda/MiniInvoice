using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CustomerEmailAddressNotFoundException : Exception
  {
    public CustomerEmailAddressNotFoundException(string message) : base(message)
    {
    }
    public CustomerEmailAddressNotFoundException(int rowId) : base($"No customerEmailAddress with id {rowId} found.")
    {
    }
  }
}
