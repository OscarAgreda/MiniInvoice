using System;
namespace DDDInvoicingClean.Domain.Exceptions
{
  public class FinancialTransactionNotFoundException : Exception
  {
    public FinancialTransactionNotFoundException(string message) : base(message)
    {
    }
    public FinancialTransactionNotFoundException(int financialTransactionId) : base($"No financialTransaction with id {financialTransactionId} found.")
    {
    }
  }
}
