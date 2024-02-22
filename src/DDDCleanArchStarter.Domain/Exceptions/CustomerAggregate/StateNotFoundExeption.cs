using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class StateNotFoundException : Exception
  {
    public StateNotFoundException(string message) : base(message)
    {
    }
    public StateNotFoundException(Guid  stateId) : base($"No state with id {stateId} found.")
    {
    }
  }
}
