using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CustomerPhoneNumberNotFoundException : Exception
  {
    public CustomerPhoneNumberNotFoundException(string message) : base(message)
    {
    }
    public CustomerPhoneNumberNotFoundException(int rowId) : base($"No customerPhoneNumber with id {rowId} found.")
    {
    }
  }
}
