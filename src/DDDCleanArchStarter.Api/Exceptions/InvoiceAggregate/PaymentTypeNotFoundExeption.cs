using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PaymentTypeNotFoundException : Exception
  {
    public PaymentTypeNotFoundException(string message) : base(message)
    {
    }
    public PaymentTypeNotFoundException(int paymentTypeId) : base($"No paymentType with id {paymentTypeId} found.")
    {
    }
  }
}
