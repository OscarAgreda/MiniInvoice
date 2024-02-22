using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class AddressNotFoundException : Exception
  {
    public AddressNotFoundException(string message) : base(message)
    {
    }
    public AddressNotFoundException(Guid  addressId) : base($"No address with id {addressId} found.")
    {
    }
  }
}
