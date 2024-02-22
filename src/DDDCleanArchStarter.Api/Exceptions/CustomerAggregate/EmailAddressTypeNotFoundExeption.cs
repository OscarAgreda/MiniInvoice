using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class EmailAddressTypeNotFoundException : Exception
  {
    public EmailAddressTypeNotFoundException(string message) : base(message)
    {
    }
    public EmailAddressTypeNotFoundException(int emailAddressTypeId) : base($"No emailAddressType with id {emailAddressTypeId} found.")
    {
    }
  }
}
