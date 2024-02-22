using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PhoneNumberTypeNotFoundException : Exception
  {
    public PhoneNumberTypeNotFoundException(string message) : base(message)
    {
    }
    public PhoneNumberTypeNotFoundException(int phoneNumberTypeId) : base($"No phoneNumberType with id {phoneNumberTypeId} found.")
    {
    }
  }
}