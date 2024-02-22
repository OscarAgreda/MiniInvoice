using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PaymentNotFoundException : Exception
  {
    public PaymentNotFoundException(string message) : base(message)
    {
    }
    public PaymentNotFoundException(int paymentId) : base($"No payment with id {paymentId} found.")
    {
    }
  }
}
