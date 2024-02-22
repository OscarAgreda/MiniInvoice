using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CustomerNotFoundException : Exception
  {
    public CustomerNotFoundException(string message) : base(message)
    {
    }
    public CustomerNotFoundException(Guid  customerId) : base($"No customer with id {customerId} found.")
    {
    }
  }
}
