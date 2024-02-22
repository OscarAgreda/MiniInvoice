using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class CityNotFoundException : Exception
  {
    public CityNotFoundException(string message) : base(message)
    {
    }
    public CityNotFoundException(Guid  cityId) : base($"No city with id {cityId} found.")
    {
    }
  }
}
