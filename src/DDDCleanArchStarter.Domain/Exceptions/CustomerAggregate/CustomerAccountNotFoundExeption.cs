using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CustomerAccountNotFoundException : Exception
  {
    public CustomerAccountNotFoundException(string message) : base(message)
    {
    }
    public CustomerAccountNotFoundException(Int32  rowId) : base($"No customerAccount with id {rowId} found.")
    {
    }
  }
}
