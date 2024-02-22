using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class EmployeePhoneNumberNotFoundException : Exception
  {
    public EmployeePhoneNumberNotFoundException(string message) : base(message)
    {
    }
    public EmployeePhoneNumberNotFoundException(int rowId) : base($"No employeePhoneNumber with id {rowId} found.")
    {
    }
  }
}
