using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class DebitTypeNotFoundException : Exception
  {
    public DebitTypeNotFoundException(string message) : base(message)
    {
    }
    public DebitTypeNotFoundException(int debitTypeId) : base($"No debitType with id {debitTypeId} found.")
    {
    }
  }
}
