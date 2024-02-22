using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class EmailAddressNotFoundException : Exception
  {
    public EmailAddressNotFoundException(string message) : base(message)
    {
    }
    public EmailAddressNotFoundException(int emailAddressId) : base($"No emailAddress with id {emailAddressId} found.")
    {
    }
  }
}
