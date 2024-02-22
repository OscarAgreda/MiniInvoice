using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class EmployeeNotFoundException : Exception
  {
    public EmployeeNotFoundException(string message) : base(message)
    {
    }
    public EmployeeNotFoundException(int employeeId) : base($"No employee with id {employeeId} found.")
    {
    }
  }
}
