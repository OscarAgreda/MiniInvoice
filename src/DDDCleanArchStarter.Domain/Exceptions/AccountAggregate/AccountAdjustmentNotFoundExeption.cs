using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AccountAdjustmentNotFoundException : Exception
  {
    public AccountAdjustmentNotFoundException(string message) : base(message)
    {
    }
    public AccountAdjustmentNotFoundException(Guid  accountAdjustmentId) : base($"No accountAdjustment with id {accountAdjustmentId} found.")
    {
    }
  }
}
