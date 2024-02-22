using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AddressTypeNotFoundException : Exception
  {
    public AddressTypeNotFoundException(string message) : base(message)
    {
    }
    public AddressTypeNotFoundException(Guid  addressTypeId) : base($"No addressType with id {addressTypeId} found.")
    {
    }
  }
}
