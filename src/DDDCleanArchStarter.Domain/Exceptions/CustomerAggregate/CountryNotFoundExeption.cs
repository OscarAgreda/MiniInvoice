using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CountryNotFoundException : Exception
  {
    public CountryNotFoundException(string message) : base(message)
    {
    }
    public CountryNotFoundException(Guid  countryId) : base($"No country with id {countryId} found.")
    {
    }
  }
}
