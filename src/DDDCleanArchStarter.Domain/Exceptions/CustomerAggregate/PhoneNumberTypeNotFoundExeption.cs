using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PhoneNumberTypeNotFoundException : Exception
  {
    public PhoneNumberTypeNotFoundException(string message) : base(message)
    {
    }
    public PhoneNumberTypeNotFoundException(Guid  phoneNumberTypeId) : base($"No phoneNumberType with id {phoneNumberTypeId} found.")
    {
    }
  }
}
