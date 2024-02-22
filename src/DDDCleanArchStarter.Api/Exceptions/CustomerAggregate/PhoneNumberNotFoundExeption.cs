using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class PhoneNumberNotFoundException : Exception
  {
    public PhoneNumberNotFoundException(string message) : base(message)
    {
    }
    public PhoneNumberNotFoundException(int phoneNumberId) : base($"No phoneNumber with id {phoneNumberId} found.")
    {
    }
  }
}
