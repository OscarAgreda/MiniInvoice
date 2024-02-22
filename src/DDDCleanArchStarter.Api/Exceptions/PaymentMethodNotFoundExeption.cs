using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PaymentMethodNotFoundException : Exception
  {
    public PaymentMethodNotFoundException(string message) : base(message)
    {
    }
    public PaymentMethodNotFoundException(int rowId) : base($"No paymentMethod with id {rowId} found.")
    {
    }
  }
}
