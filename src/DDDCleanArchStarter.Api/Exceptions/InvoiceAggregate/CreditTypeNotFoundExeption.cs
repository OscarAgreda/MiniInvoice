using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CreditTypeNotFoundException : Exception
  {
    public CreditTypeNotFoundException(string message) : base(message)
    {
    }
    public CreditTypeNotFoundException(int creditTypeId) : base($"No creditType with id {creditTypeId} found.")
    {
    }
  }
}
