using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AccountNotFoundException : Exception
  {
    public AccountNotFoundException(string message) : base(message)
    {
    }
    public AccountNotFoundException(int accountId) : base($"No account with id {accountId} found.")
    {
    }
  }
}
