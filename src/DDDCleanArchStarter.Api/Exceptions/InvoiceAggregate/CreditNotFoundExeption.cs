using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CreditNotFoundException : Exception
  {
    public CreditNotFoundException(string message) : base(message)
    {
    }
    public CreditNotFoundException(int creditId) : base($"No credit with id {creditId} found.")
    {
    }
  }
}
