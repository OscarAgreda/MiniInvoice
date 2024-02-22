using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class DebitNotFoundException : Exception
  {
    public DebitNotFoundException(string message) : base(message)
    {
    }
    public DebitNotFoundException(int debitId) : base($"No debit with id {debitId} found.")
    {
    }
  }
}
