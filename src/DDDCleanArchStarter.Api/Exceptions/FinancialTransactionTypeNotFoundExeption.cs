using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class FinancialTransactionTypeNotFoundException : Exception
  {
    public FinancialTransactionTypeNotFoundException(string message) : base(message)
    {
    }
    public FinancialTransactionTypeNotFoundException(int financialTransactionTypeId) : base($"No financialTransactionType with id {financialTransactionTypeId} found.")
    {
    }
  }
}
