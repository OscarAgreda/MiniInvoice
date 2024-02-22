using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AccountTypeNotFoundException : Exception
  {
    public AccountTypeNotFoundException(string message) : base(message)
    {
    }
    public AccountTypeNotFoundException(int accountTypeId) : base($"No accountType with id {accountTypeId} found.")
    {
    }
  }
}
