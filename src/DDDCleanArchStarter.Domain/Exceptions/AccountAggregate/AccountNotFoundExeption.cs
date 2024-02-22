using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AccountNotFoundException : Exception
  {
    public AccountNotFoundException(string message) : base(message)
    {
    }
    public AccountNotFoundException(Guid  accountId) : base($"No account with id {accountId} found.")
    {
    }
  }
}
